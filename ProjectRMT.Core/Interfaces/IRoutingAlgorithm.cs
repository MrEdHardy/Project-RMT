using Project_RMT.Models;

namespace Project_RMT.Interfaces
{
    public interface IRoutingAlgorithm
    {
        void UpdateRoutingTables(ref IEnumerable<Router> routers);
    }
}