using Microsoft.AspNetCore.SignalR;
using SCADA.DTOS;
using SCADA.Hubs;
using SCADA.Hubs.IHubs;
using SCADA.Model;
using SCADA.Repository.IRepository;

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
            for (int i = 1; i < 5; i++)
            {
                Tag? tag = await tagRepository.GetInputByAddress(i.ToString());
                if (i % 3 == 0)
                {
                    startThread(tag, i.ToString(), "S");
                }
                else if (i % 3 == 1)
                {
                    startThread(tag, i.ToString(), "C");
                }
                else
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
                double val = SimulationDriver.SimulationDriver.ReturnValue(function);
                // await _simulationHub.Clients.All.SendSimulationData(new SimulationDataDTO(address, val));
                if (tag is not null)
                {
                    if (tag is AnalogInput analog)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(analog.ScanTime));

                    }
                    else if (tag is DigitalInput digital)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(digital.ScanTime));
                    }
                }
                else
                {
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                }

            }
        });
        thread.Start();
        _scanTimers[address] = thread;
    }
}