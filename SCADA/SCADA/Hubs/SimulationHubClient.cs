using Microsoft.AspNetCore.SignalR;
using SCADA.Hubs.IHubs;

namespace SCADA.Hubs;

public class SimulationHubClient : Hub<ISimulationHubClient>
{
    public SimulationHubClient() { }

    public async Task SendSimulationData(string data)
    {
        await Clients.All.SendSimulationData(data);
    }
}