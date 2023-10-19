using Project_RMT.Interfaces;
using Project_RMT.Models;

namespace Project_RMT.Core
{
    public sealed class OspfAlgorithm : IRoutingAlgorithm
    {
        public bool wasInitialized = false;

        private void InitRoutingTables(ref IEnumerable<Router> routers)
        {
            throw new NotImplementedException();
        }

        public void UpdateRoutingTables(IEnumerable<Router> routers)
        {
            if (!wasInitialized)
            {
                this.InitRoutingTables(ref routers);
                wasInitialized = true;
            }
        }
    }
}
