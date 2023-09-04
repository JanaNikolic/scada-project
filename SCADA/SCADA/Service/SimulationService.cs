using Microsoft.AspNetCore.SignalR;
using SCADA.DTOS;
using SCADA.Hubs;
using SCADA.Hubs.IHubs;
using SCADA.Model;
using SCADA.Repository.IRepository;
using SCADA.Service.IService;

namespace SCADA.Service;

public class SimulationService : BackgroundService
{

    private readonly IServiceProvider _serviceProvider;
    private readonly Dictionary<string, Thread> _scanTimers = new Dictionary<string, Thread>();
    private readonly IHubContext<SimulationHubClient, ISimulationHubClient> _simulationHub;

    public SimulationService(IServiceProvider serviceProvider, IHubContext<SimulationHubClient, ISimulationHubClient> hubContext)
    {
        _serviceProvider = serviceProvider;
        _simulationHub = hubContext;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await StartSimulation(stoppingToken);
    }

    private async Task StartSimulation(CancellationToken stoppingToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var tagRepository = scope.ServiceProvider.GetRequiredService<ITagRepository>();
            var tags = await tagRepository.GetSimulationDriverTags();
            foreach (var tag in tags)
            {
                int i;
                if (int.TryParse(tag.IOAddress, out i) && i % 3 == 0)
                {
                    startThread(tag, i.ToString(), "S");
                }
                else if (int.TryParse(tag.IOAddress, out i) && i % 3 == 1)
                {
                    startThread(tag, i.ToString(), "C");
                }
                else if (int.TryParse(tag.IOAddress, out i))
                {
                    startThread(tag, i.ToString(), "R");
                }
            }
        }
    }
    
    private void startThread(Tag? tag, string address, string function)
    {
        if (_scanTimers.ContainsKey(address))
        {
            return;
        }
        Thread thread = new Thread(async () =>
        {
            while (true)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var tagRepository = scope.ServiceProvider.GetRequiredService<ITagRepository>();
                    var alarmService = scope.ServiceProvider.GetRequiredService<IAlarmService>();
                    
                    double val = SimulationDriver.SimulationDriver.ReturnValue(function);
                    
                    if (tag is not null)
                    {
                        if (tag is AnalogInput analog)
                        {
                            var tagRecord = new TagRecord(tag, val, tag.IOAddress);
                            tag.Value = val;
                            tagRepository.UpdateTag(tag);
                            tagRepository.AddTagRecord(tagRecord);
                            await _simulationHub.Clients.All.SendSimulationData(tagRecord);
                            Thread.Sleep(TimeSpan.FromSeconds(analog.ScanTime));
                            
                            CheckIfAlarmExists(tagRecord, analog, alarmService);
                            
                        }
                        else if (tag is DigitalInput digital)
                        {
                            val = (int)Math.Round(val % 2, 2, MidpointRounding.AwayFromZero);
                            if (val < 0)
                            {
                                val = 0;
                            }
                            else if (val > 1)
                            {
                                val = 1;
                            }
                            var tagRecord = new TagRecord(tag, val, tag.IOAddress);
                            tag.Value = val;
                            tagRepository.UpdateTag(tag);
                            tagRepository.AddTagRecord(tagRecord);
                            await _simulationHub.Clients.All.SendSimulationData(tagRecord);
                            Thread.Sleep(TimeSpan.FromSeconds(digital.ScanTime));
                        }
                    }
                    else
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(5));
                    }
                }
            }
        });
        thread.Start();
        _scanTimers[address] = thread;
    }
    
    private void CheckIfAlarmExists(TagRecord value, AnalogInput analog, IAlarmService alarmService)
    {
        foreach(var alarm in analog.Alarms)
        {
            Alarm a = alarmService.GetById(alarm.Id);
            if ((alarm.Type == Alarm.AlarmType.ABOVE && alarm.Threshold >= value.Value) || (alarm.Type == Alarm.AlarmType.BELOW && alarm.Threshold <= value.Value))
            {
                alarmService.AddAlarmValue(new AlarmActivated(a), analog);
            }
        }
    }
}