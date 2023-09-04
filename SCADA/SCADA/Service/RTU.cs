using Microsoft.AspNetCore.SignalR;
using SCADA.Hubs;
using SCADA.Hubs.IHubs;
using SCADA.Model;
using SCADA.Repository.IRepository;
using SCADA.Service.IService;

namespace SCADA.Service;

public class RTU : BackgroundService
{
	private readonly IServiceProvider _serviceProvider;
	private Random random = new Random();
    private readonly IHubContext<RTUHubClient,IRTUHubClient> _rtuHub;

	public RTU(IServiceProvider serviceProvider, IHubContext<RTUHubClient,IRTUHubClient> rtuHub)
    {
        _serviceProvider = serviceProvider;
        _rtuHub = rtuHub;
    }

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using(var scope = _serviceProvider.CreateScope())
            {
                var tagRepository = scope.ServiceProvider.GetRequiredService<ITagRepository>();
                var alarmService = scope.ServiceProvider.GetRequiredService<IAlarmService>();

                var inputTags = await tagRepository.GetRTUInputsAsync();
                foreach (var tag in inputTags)
                {
                    double value;
                    if(tag == null) { continue; }
                    
                    if(tag is AnalogInput analogInput)
                    {
                        value = analogInput.LowLimit + (random.NextDouble() * (analogInput.HighLimit - analogInput.LowLimit));
						value = Math.Round(value, 2, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        value = random.NextInt64(0, 2);
                        value = Math.Round(value, 2, MidpointRounding.AwayFromZero);
                    }
                    var tagRecord = new TagRecord(tag, value, tag.IOAddress);
                    tag.Value = value;
                    tagRepository.UpdateTag(tag);
                    tagRepository.AddTagRecord(tagRecord);
                    await _rtuHub.Clients.All.SendRTUData(tagRecord);
                    if(tag is AnalogInput analog)
                    {
                        CheckIfAlarmExists(tagRecord, analog, alarmService);
                    }
                }

            }

            await Task.Delay(10000, stoppingToken);
        }
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