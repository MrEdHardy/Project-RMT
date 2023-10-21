using Project_RMT.Interfaces;
using Project_RMT.Models;

namespace Project_RMT.Core
{
    public class RipAlgorithm : IRoutingAlgorithm
    {
        public void UpdateRoutingTables(ref IEnumerable<Router> routers)
        {
            foreach (var router in routers)
            {
                foreach (var networkInterface in router.NetworkInterfaces)
                {
                    if (networkInterface.ConnectedNetworkDevice is Router connectedRouter)
                    {
                        foreach (var incomingEntry in router.RoutingTable)
                        {
                            var existingEntry = connectedRouter.RoutingTable.FirstOrDefault(e => e.TargetIPAdress == incomingEntry.TargetIPAdress);

                            if (existingEntry == null)
                            {
                                connectedRouter.RoutingTable.Add(new RoutingEntry
                                {
                                    TargetIPAdress = incomingEntry.TargetIPAdress,
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
                                    existingEntry.TargetIPAdress = router.IPAdress;
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
