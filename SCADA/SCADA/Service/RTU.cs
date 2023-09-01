using SCADA.Model;
using SCADA.Repository.IRepository;

namespace SCADA.Service;

public class RTU : BackgroundService
{
	private readonly IServiceProvider _serviceProvider;
	private Random random = new Random();

	public RTU(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using(var scope = _serviceProvider.CreateScope())
            {
                var tagRepository = scope.ServiceProvider.GetRequiredService<ITagRepository>();

                var inputTags = await tagRepository.GetInputsAsync();
                for(int i = 5; i < 11; i++)
                {
                    double value;
                    Tag? tag = inputTags.FirstOrDefault(t => t.IOAddress == i.ToString());
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
                    var tagId = tag == null ? 0 : tag.Id;
                    tag.Id = tagId;
                    var tagRecord = new TagRecord(tag, value, tag.IOAddress);
                    tagRecord.Timestamp = DateTime.UtcNow;
                    tag.Value = value;
                    tagRepository.UpdateTag(tag);
                    tagRepository.AddTagRecord(tagRecord);
                }

            }

            await Task.Delay(10000, stoppingToken);
        }
    }

}