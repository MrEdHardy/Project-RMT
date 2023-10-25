using Project_RMT.Interfaces;
using Project_RMT.Models;

namespace Project_RMT.Core
{
    public class RipAlgorithm : IRoutingAlgorithm
    {
        public void UpdateRoutingTables(ref ICollection<Router> routers)
        {
            foreach (var router in routers)
            {
                foreach (var networkInterface in router.NetworkInterfaces)
                {
                    if (networkInterface.ConnectedNetworkDevice is Router connectedRouter)
                    {
                        foreach (var incomingEntry in router.RoutingTable)
                        {
                            // Prüfen, ob der Eintrag des empfangenen Routers er selbst ist
                            if (incomingEntry.TargetIPAdress.Equals(connectedRouter.IPAdress)) continue;

                            var existingEntry = connectedRouter.RoutingTable
                                .FirstOrDefault(e => e.TargetIPAdress.Equals(incomingEntry.TargetIPAdress));

                            // Sollten theoretisch die Kosten zwischen benachbarten Routern nicht 1 betragen?
                            int newMetric = incomingEntry.Metric + 1;

                            if (existingEntry == null || newMetric < existingEntry.Metric)
                            {
                                if (existingEntry == null)
                                {
                                    connectedRouter.RoutingTable.Add(new RoutingEntry
                                    {
                                        TargetIPAdress = incomingEntry.TargetIPAdress,
                                        NextHop = router.IPAdress,
                                        NetworkInterface = incomingEntry.NetworkInterface,
                                        Metric = newMetric,
                                        IsActive = true
                                    });
                                }
                                else
                                {
                                    existingEntry.NextHop = router.IPAdress;
                                    existingEntry.NetworkInterface = incomingEntry.NetworkInterface;
                                    existingEntry.Metric = newMetric;
                                    existingEntry.IsActive = true;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
