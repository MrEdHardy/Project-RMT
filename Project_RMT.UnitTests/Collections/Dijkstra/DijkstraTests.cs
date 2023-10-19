using Project_RMT.Collections;
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
        }

        [Fact]
        public void TestOSPF()
        {
            var router1 = new Router
            {
                Id = 1,
                IPAdress = new System.Net.IPAddress(new byte[] { 192, 168, 0, 0 }),
                Clients = new List<Client>(),
                NetworkInterfaces = new List<NetworkInterface>(),
                RoutingTable = new List<RoutingEntry>()
            };

            // Trial tomorrow
            var node1 = new Node<INetworkDevice>(router1);
        }
    }
}
