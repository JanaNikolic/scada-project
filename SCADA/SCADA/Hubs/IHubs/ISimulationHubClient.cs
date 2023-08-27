namespace SCADA.Hubs.IHubs;

public interface ISimulationHubClient
{
    Task SendSimulationData(string data);
}