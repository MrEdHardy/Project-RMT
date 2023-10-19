using Project_RMT.Collections;
using Project_RMT.Interfaces;
using Project_RMT.Models;

namespace Project_RMT.Core
{
    public sealed class OspfAlgorithm : IRoutingAlgorithm
    {
        public bool GraphInitialized = false;
        private Graph<INetworkDevice>? networkgraph;

        public void UpdateRoutingTables(ref IEnumerable<Router> routers)
        {
            if (!this.GraphInitialized)
            {
                var nodeList = new List<Node<INetworkDevice>>();

                foreach (var router in routers)
                {
                    var routerNode = new Node<INetworkDevice>(router);

                    foreach (var connectedInterface in router.NetworkInterfaces)
                    {
                        if (connectedInterface.ConnectedNetworkDevice is not null)
                        {
                            var connectedDeviceNode = new Node<INetworkDevice>(connectedInterface.ConnectedNetworkDevice);
                            var metric = router.RoutingTable.Single(entry => connectedInterface.ConnectedNetworkDevice.Id == entry.NetworkInterface.ConnectedNetworkDevice?.Id).Metric;
                                
                            routerNode.Edges.Add(new Edge<INetworkDevice>(connectedDeviceNode, metric));
                            connectedDeviceNode.Edges.Add(new Edge<INetworkDevice>(routerNode, metric));

                            if (!nodeList.Contains(connectedDeviceNode))
                            {
                                nodeList.Add(connectedDeviceNode);
                            }
                        }
                    }
                    nodeList.Add(routerNode);
                }

                this.networkgraph = new Graph<INetworkDevice>(nodeList);
                this.GraphInitialized = true;
            }

            foreach (var router in routers)
            {
                if (this.networkgraph is not null)
                {
                    var index = ((List<Node<INetworkDevice>>)this.networkgraph.Nodes).FindIndex(nodes => nodes.Value.Id == router.Id);
                    var shortestPaths = this.networkgraph.FindShortestPath(((List<Node<INetworkDevice>>)this.networkgraph.Nodes)[index]);

                    // Morgen mehr!

                    //foreach (var path in shortestPaths)
                    //{
                    //    if ()
                    //}
                }
            }
        }
    }
}
