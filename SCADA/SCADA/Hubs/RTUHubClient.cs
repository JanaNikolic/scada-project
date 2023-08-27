using Microsoft.AspNetCore.SignalR;
using SCADA.Hubs.IHubs;

namespace SCADA.Hubs;

public class RTUHubClient : Hub<IRTUHubClient>
{
    public RTUHubClient()
    {
    }

    public async Task SendRTUData(string data)
    {
        await Clients.All.SendRTUData(data);
    }
}