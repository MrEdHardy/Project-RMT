using Project_RMT.Interfaces;
using Project_RMT.Models;

namespace Project_RMT.Core
{
    public class SimulationEngine
    {
        private readonly IRoutingAlgorithm _routingalgorithm;

        public List<INetworkDevice>? devices { get; set; } = new List<INetworkDevice>();

        public SimulationEngine(IRoutingAlgorithm routingalgorithm)
        {
            this._routingalgorithm = routingalgorithm;
        }

        public void UpdateRoutingtables()
        {
            if (devices is not null && devices.Where(device => device is Router) is List<Router> router)
            {
                this._routingalgorithm.UpdateRoutingTables(router);
            }
        }
    }
}
