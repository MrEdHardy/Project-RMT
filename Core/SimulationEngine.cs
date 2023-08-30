using Project_RMT.Interfaces;
using Project_RMT.Models;

namespace Project_RMT.Core
{
    public class SimulationEngine
    {
        private readonly IRoutingAlgorithm routingalgorithm;

        public List<INetworkDevice>? Devices { get; set; } = new List<INetworkDevice>();

        public SimulationEngine(IRoutingAlgorithm routingalgorithm)
        {
            this.routingalgorithm = routingalgorithm;
        }

        public void UpdateRoutingtables()
        {
            if (Devices is not null && Devices.Where(device => device is Router) is List<Router> router)
            {
                this.routingalgorithm.UpdateRoutingTables(router);
            }
        }
    }
}
