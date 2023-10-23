using Project_RMT.Collections;
using Project_RMT.Core;
using Project_RMT.Interfaces;
using Project_RMT.Models;
using Project_RMT.UnitTests.Common;

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
            var preparedNetworkStructure = UnitTestHelpers.PrepareBaseNetworkstructure(generateMetric: true);

            var router1 = preparedNetworkStructure.routerList.First();
            var router3 = preparedNetworkStructure.routerList.Last();
            var client1 = preparedNetworkStructure.clientList.First();
            var client2 = preparedNetworkStructure.clientList.Last();

            var countBeforeRouter1 = router1.RoutingTable.Count;
            var countBeforeRouter3 = router3.RoutingTable.Count;

            Assert.DoesNotContain(router1.RoutingTable, re => re.TargetIPAdress == client2.IPAdress);

            var testOSPFAlgo = new OspfAlgorithm();
            testOSPFAlgo.UpdateRoutingTables(ref preparedNetworkStructure.routerList);

            var countAfterRouter1 = router1.RoutingTable.Count;
            var countAfterRouter3 = router3.RoutingTable.Count;

            Assert.True(countBeforeRouter1 < countAfterRouter1);
            Assert.True(countBeforeRouter3 < countAfterRouter3);
            Assert.Contains(router1.RoutingTable, re => re.TargetIPAdress == client2.IPAdress);
            Assert.True(router1.RoutingTable.Single(re => re.TargetIPAdress == client2.IPAdress).Metric == 17);

            Assert.Contains(router3.RoutingTable, re => re.TargetIPAdress == client1.IPAdress);
            Assert.Contains(router3.RoutingTable, re => re.TargetIPAdress == client2.IPAdress);
            Assert.True(router3.RoutingTable.Single(re => re.TargetIPAdress == client1.IPAdress).Metric == 7);
            Assert.True(router3.RoutingTable.Single(re => re.TargetIPAdress == client2.IPAdress).Metric == 12);
        }
    }
}
