using Project_RMT.Collections;
using Project_RMT.Core;
using Project_RMT.Interfaces;
using Project_RMT.Models;

namespace Project_RMT.UnitTests.Collections.Dijkstra
{
    public class DijkstraTests
    {
        [Fact]
        public void ShouldFindClostestSimpleNodeDistance()
        {
            var node1 = new Node<int>(0);
            var node2 = new Node<int>(100);
            var node3 = new Node<int>(250);

            node1.Edges.Add(new Edge<int>(node2, 100));
            node2.Edges.Add(new Edge<int>(node3, 150));
            node3.Edges.Add(new Edge<int>(node3, 0));

            var graph = new Graph<int>(new[] { node1, node2, node3 });

            var distances = graph.FindShortestPath(node1);

            Assert.True(distances.Count > 0);
            Assert.True(distances.Count == 3);
            Assert.True(distances.Single(s => s.TargetNode == node2).TotalCost == node1.Edges.First().Weight);
            Assert.True(node2.Edges.First().Weight + node1.Edges.First().Weight == distances.Single(n => n.TargetNode == node3).TotalCost);
        }

        /// <summary>
        /// Special Scenario contanining three Routers and two Clients <br></br>
        /// R1 -> C1, R1 -> R2, R1 -> R3, R2 -> C1, R2 -> C2, R2 -> R3; Connections are uni-directional
        /// </summary>
        [Fact]
        public void ShouldTestIfOspfUpdatesRoutingTablesAccordingly()
        {
            var router1 = new Router
            {
                Id = 1,
                IPAdress = new System.Net.IPAddress(new byte[] { 192, 168, 0, 0 }),
                Clients = new List<Client>(),
                NetworkInterfaces = new List<NetworkInterface>(),
                RoutingTable = new List<RoutingEntry>()
            };

            var router2 = new Router
            {
                Id = 2,
                IPAdress = new System.Net.IPAddress(new byte[] { 192, 168, 0, 1 }),
                Clients = new List<Client>(),
                NetworkInterfaces = new List<NetworkInterface>(),
                RoutingTable = new List<RoutingEntry>()
            };

            var router3 = new Router
            {
                Id = 5,
                IPAdress = new System.Net.IPAddress(new byte[] { 192, 168, 0, 2 }),
                Clients = new List<Client>(),
                NetworkInterfaces = new List<NetworkInterface>(),
                RoutingTable = new List<RoutingEntry>()
            };

            var client1 = new Client
            {
                Id = 3,
                IPAdress = new System.Net.IPAddress(new byte[] { 192, 168, 1, 0 }),
                NetworkInterfaces = new List<NetworkInterface>()
            };

            var client2 = new Client
            {
                Id = 4,
                IPAdress = new System.Net.IPAddress(new byte[] { 192, 168, 1, 1 }),
                NetworkInterfaces = new List<NetworkInterface>()
            };

            var router1Client1Connection = new NetworkInterface
            {
                ConnectedNetworkDevice = client1,
                Name = "IF0"
            };

            router1.NetworkInterfaces.Add(router1Client1Connection);
            router1.RoutingTable.Add(new RoutingEntry 
            {
                NetworkInterface = router1Client1Connection,
                TargetIPAdress = client1.IPAdress,
                Metric = 20,
                IsActive = true
            });

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

            IEnumerable<Router> routerList = new List<Router>();
            ((List<Router>)routerList).AddRange(new Router[] { router1, router2, router3 });

            var countBefore = router1.RoutingTable.Count;
            Assert.DoesNotContain(router1.RoutingTable, re => re.TargetIPAdress == client2.IPAdress);

            var testOSPFAlgo = new OspfAlgorithm();
            testOSPFAlgo.UpdateRoutingTables(ref routerList);

            var countAfter = router1.RoutingTable.Count;

            Assert.True(countBefore < countAfter);
            Assert.Contains(router1.RoutingTable, re => re.TargetIPAdress == client2.IPAdress);
        }
    }
}
