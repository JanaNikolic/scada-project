using Microsoft.AspNetCore.SignalR;
using SCADA.Hubs;
using SCADA.Hubs.IHubs;
using SCADA.Model;
using SCADA.Repository.IRepository;

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
                }

            }

            await Task.Delay(10000, stoppingToken);
        }
    }

}