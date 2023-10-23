using Project_RMT.Interfaces;
using Project_RMT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_RMT.UnitTests.Common
{
    internal static class UnitTestHelpers
    {
        public static (ICollection<Router> routerList, ICollection<Client> clientList) PrepareBaseNetworkstructure(bool generateMetric = false)
        {
            var router1 = new Router
            {
                Id = Guid.NewGuid(),
                IPAdress = new System.Net.IPAddress(new byte[] { 192, 168, 0, 0 }),
                Clients = new(),
                NetworkInterfaces = new(),
                RoutingTable = new()
            };

            var router2 = new Router
            {
                Id = Guid.NewGuid(),
                IPAdress = new System.Net.IPAddress(new byte[] { 192, 168, 0, 1 }),
                Clients = new(),
                NetworkInterfaces = new(),
                RoutingTable = new()
            };

            var router3 = new Router
            {
                Id = Guid.NewGuid(),
                IPAdress = new System.Net.IPAddress(new byte[] { 192, 168, 0, 2 }),
                Clients = new(),
                NetworkInterfaces = new(),
                RoutingTable = new()
            };

            var client1 = new Client
            {
                Id = Guid.NewGuid(),
                IPAdress = new System.Net.IPAddress(new byte[] { 192, 168, 1, 0 }),
                NetworkInterfaces = new()
            };

            var client2 = new Client
            {
                Id = Guid.NewGuid(),
                IPAdress = new System.Net.IPAddress(new byte[] { 192, 168, 1, 1 }),
                NetworkInterfaces = new()
            };

            var router1Client1Connection = new NetworkInterface
            {
                ConnectedNetworkDevice = client1,
                Name = "IF0"
            };

            router1.NetworkInterfaces.Add(router1Client1Connection);

            var router1Router2Connection = new NetworkInterface
            {
                ConnectedNetworkDevice = router2,
                Name = "IF1"
            };
            var router1Router3Connection = new NetworkInterface
            {
                ConnectedNetworkDevice = router3,
                Name = "IF3"
            };


            router1.NetworkInterfaces.Add(router1Router2Connection);
            router1.NetworkInterfaces.Add(router1Router3Connection);

            var router2Router1Connection = new NetworkInterface
            {
                ConnectedNetworkDevice = router1,
                Name = "IF0"
            };
            var router2Client2Connection = new NetworkInterface
            {
                ConnectedNetworkDevice = client2,
                Name = "IF1"
            };
            var router2Router3Connection = new NetworkInterface
            {
                ConnectedNetworkDevice = router3,
                Name = "IF2"
            };
            var router2Client1Connection = new NetworkInterface
            {
                ConnectedNetworkDevice = client1,
                Name = "IF3"
            };

            router2.NetworkInterfaces.Add(router2Router1Connection);
            router2.NetworkInterfaces.Add(router2Client2Connection);
            router2.NetworkInterfaces.Add(router2Router3Connection);
            router2.NetworkInterfaces.Add(router2Client1Connection);

            var router3Router1Connection = new NetworkInterface
            {
                ConnectedNetworkDevice = router1,
                Name = "IF0"
            };
            var router3Router2Connection = new NetworkInterface
            {
                ConnectedNetworkDevice = router2,
                Name = "IF1"
            };

            router3.NetworkInterfaces.Add(router3Router1Connection);
            router3.NetworkInterfaces.Add(router3Router2Connection);
            
            router1.RoutingTable.Add(new RoutingEntry
            {
                NetworkInterface = router1Client1Connection,
                TargetIPAdress = client1.IPAdress,
                Metric = 20,
                IsActive = true
            });

            router1.RoutingTable.Add(new RoutingEntry
            {
                NetworkInterface = router1Router2Connection,
                TargetIPAdress = router2.IPAdress,
                Metric = 10,
                IsActive = true
            });
            router1.RoutingTable.Add(new RoutingEntry
            {
                NetworkInterface = router1Router3Connection,
                TargetIPAdress = router3.IPAdress,
                Metric = 5,
                IsActive = true
            });

            router2.RoutingTable.Add(new RoutingEntry
            {
                NetworkInterface = router2Router1Connection,
                TargetIPAdress = router1.IPAdress,
                Metric = 10,
                IsActive = true
            });
            router2.RoutingTable.Add(new RoutingEntry
            {
                NetworkInterface = router2Client2Connection,
                TargetIPAdress = client2.IPAdress,
                Metric = 10,
                IsActive = true
            });
            router2.RoutingTable.Add(new RoutingEntry
            {
                NetworkInterface = router2Router3Connection,
                TargetIPAdress = router3.IPAdress,
                Metric = 2,
                IsActive = true
            });
            router2.RoutingTable.Add(new RoutingEntry
            {
                NetworkInterface = router2Client1Connection,
                TargetIPAdress = client1.IPAdress,
                Metric = 5,
                IsActive = true
            });

            router3.RoutingTable.Add(new RoutingEntry
            {
                NetworkInterface = router3Router1Connection,
                TargetIPAdress = router1.IPAdress,
                Metric = 5,
                IsActive = true
            });
            router3.RoutingTable.Add(new RoutingEntry
            {
                NetworkInterface = router3Router2Connection,
                TargetIPAdress = router2.IPAdress,
                Metric = 2,
                IsActive = true
            });

            if (!generateMetric) 
            {
                foreach (var routingEntry in router1.RoutingTable)
                {
                    routingEntry.Metric = 0;
                }
                
                foreach (var routingEntry in router2.RoutingTable)
                {
                    routingEntry.Metric = 0;
                }
                
                foreach (var routingEntry in router3.RoutingTable)
                {
                    routingEntry.Metric = 0;
                }
            }

            ICollection<Router> routerList = new List<Router>();
            ((List<Router>)routerList).AddRange(new Router[] { router1, router2, router3 });
            return (routerList, new List<Client>(new Client[] { client1, client2 }));
        }
    }
}
