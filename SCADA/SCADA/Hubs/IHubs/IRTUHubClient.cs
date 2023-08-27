namespace SCADA.Hubs.IHubs;

public interface IRTUHubClient
{
    Task SendRTUData(string data);
}