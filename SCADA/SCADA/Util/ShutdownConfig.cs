using SCADA.Service.IService;

namespace SCADA.Util;

public class ShutdownConfig: IHostedService, IDisposable
{
    // private readonly ITagService _tagService;
    // private readonly IAlarmService _alarmService;
    private readonly IServiceProvider _serviceProvider;    

    // public ShutdownConfig(ITagService tagService, IAlarmService alarmService)
    // {
    //     _tagService = tagService;
    //     _alarmService = alarmService;
    // }

    public ShutdownConfig(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var tagService = scope.ServiceProvider.GetRequiredService<ITagService>();
            var alarmService = scope.ServiceProvider.GetRequiredService<IAlarmService>();

            var tags = tagService.GetTags();

            var tagsXml = XMLConfig.SerializeToXmlTags(tags);
            await File.WriteAllTextAsync("Config/scadaConfig.xml", tagsXml);

            var alarms = alarmService.GetAll();
            var alarmsXml = XMLConfig.SerializeToXmlAlarms(alarms);
            await File.WriteAllTextAsync("Config/alarmsConfig.xml", alarmsXml);
        }
    }

    public void Dispose()
    {
        // Dispose resources here if needed.
    }

}