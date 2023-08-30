using Project_RMT.Interfaces;
using Project_RMT.Models;
using System.Net;

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
                    if (networkInterface.ConnectedRouter is Router connectedRouter)
                    {
                        foreach (var incomingEntry in router.RoutingTable)
                        {
                            var existingEntry = connectedRouter.RoutingTable.Find(e => e.Network == incomingEntry.Network);

                            if (existingEntry == null)
                            {
                                connectedRouter.RoutingTable.Add(new RoutingEntry
                                {
                                    IPAddress = entry.IPAddress,
                                    NextHop = neighboringRouter.RoutingTable[0].NextHop,
                                    Metric = entry.Metric + 1
                                });
                            }
                            else
                            {
                                if (entry.Metric + 1 < existingEntry.Metric)
                                {
                                    existingEntry.NextHop = neighboringRouter.RoutingTable[0].NextHop;
                                    existingEntry.Metric = entry.Metric + 1;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
