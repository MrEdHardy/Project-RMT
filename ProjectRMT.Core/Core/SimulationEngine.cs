using Project_RMT.Interfaces;
using Project_RMT.Models;

namespace Project_RMT.Core
{
    public class SimulationEngine
    {
        private readonly IRoutingAlgorithm routingalgorithm;

        public List<INetworkDevice?> Devices { get; set; } = new List<INetworkDevice?>();

        public SimulationEngine(IRoutingAlgorithm routingalgorithm)
        {
            this.routingalgorithm = routingalgorithm;
        }

        public void UpdateRoutingtables()
        {
            if (Devices.Where(device => device is Router) is IEnumerable<Router> routers)
            {
                this.routingalgorithm.UpdateRoutingTables(ref routers);
            }
        }
    }
}
