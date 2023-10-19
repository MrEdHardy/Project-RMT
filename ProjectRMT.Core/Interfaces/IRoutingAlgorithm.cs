using Project_RMT.Models;

namespace Project_RMT.Interfaces
{
    public interface IRoutingAlgorithm
    {
        void UpdateRoutingTables(IEnumerable<Router> routers);
    }
}