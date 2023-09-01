using Project_RMT.Interfaces;
using Project_RMT.Models;

namespace Project_RMT.Core
{
    public class RipAlgorithm : IRoutingAlgorithm
    {
        public void UpdateRoutingTables(IEnumerable<Router> routers)
        {
            foreach (var router in routers)
            {
                foreach (var networkInterface in router.NetworkInterfaces)
                {
                    if (networkInterface.ConnectedNetworkDevice is Router connectedRouter)
                    {
                        foreach (var incomingEntry in router.RoutingTable)
                        {
                            var existingEntry = connectedRouter.RoutingTable.FirstOrDefault(e => e.Network == incomingEntry.Network);

                            if (existingEntry == null)
                            {
                                connectedRouter.RoutingTable.Add(new RoutingEntry
                                {
                                    Network = incomingEntry.Network,
                                    NextHop = router.IPAdress,
                                    Metric = incomingEntry.Metric + 1,
                                    NetworkInterface = connectedRouter.NetworkInterfaces.First(r => r.ConnectedNetworkDevice?.IPAdress == router.IPAdress),
                                });
                            }
                            else
                            {
                                if (incomingEntry.Metric + 1 < existingEntry.Metric)
                                {
                                    existingEntry.NextHop = router.IPAdress;
                                    existingEntry.Metric = incomingEntry.Metric + 1;
                                    existingEntry.Network = router.IPAdress;
                                    existingEntry.NetworkInterface = connectedRouter.NetworkInterfaces.First(r => r.ConnectedNetworkDevice?.IPAdress == router.IPAdress);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
