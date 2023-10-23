using Project_RMT.Collections;
using Project_RMT.Interfaces;
using Project_RMT.Models;

namespace Project_RMT.Core
{
    public sealed class OspfAlgorithm : IRoutingAlgorithm
    {
        public bool GraphInitialized = false;
        private Graph<INetworkDevice>? networkgraph;

        public void UpdateRoutingTables(ref ICollection<Router> routers)
        {
            if (!this.GraphInitialized)
            {
                var nodeDictionary = new Dictionary<INetworkDevice, Node<INetworkDevice>>();

                foreach (var router in routers)
                {
                    if (!nodeDictionary.TryGetValue(router, out var routerNode))
                    {
                        routerNode = new Node<INetworkDevice>(router);
                        nodeDictionary[router] = routerNode;
                    }

                    foreach (var connectedInterface in router.NetworkInterfaces)
                    {
                        if (connectedInterface.ConnectedNetworkDevice is not null)
                        {
                            if (!nodeDictionary.TryGetValue(connectedInterface.ConnectedNetworkDevice, out var connectedDeviceNode))
                            {
                                connectedDeviceNode = new Node<INetworkDevice>(connectedInterface.ConnectedNetworkDevice);
                                nodeDictionary[connectedInterface.ConnectedNetworkDevice] = connectedDeviceNode;
                            }

                            var metric = router.RoutingTable.Single(entry => connectedInterface.ConnectedNetworkDevice.Id == entry.NetworkInterface.ConnectedNetworkDevice?.Id).Metric;

                            routerNode.Edges.Add(new Edge<INetworkDevice>(connectedDeviceNode, metric));
                            connectedDeviceNode.Edges.Add(new Edge<INetworkDevice>(routerNode, metric));
                        }
                    }
                }
                this.networkgraph = new Graph<INetworkDevice>(nodeDictionary.Values.ToList());
                this.GraphInitialized = true;
            }

            foreach (var router in routers)
            {
                if (this.networkgraph is not null)
                {
                    var shortestPaths = this.networkgraph.FindShortestPath(this.networkgraph.Nodes.Single(n => n.Value.Id == router.Id));

                    foreach (var path in shortestPaths)
                    {
                        if (path.TargetNode.Value.Id != router.Id 
                            && !router.NetworkInterfaces.Any(c => c.ConnectedNetworkDevice?.Id == path.TargetNode.Value.Id) 
                            && !router.RoutingTable.Any(re => re.TargetIPAdress.Equals(path.TargetNode.Value.IPAdress)))
                        {
                            var nextHop = path.Path.ElementAt(path.Path.FindIndex(n => n.Value.Id == router.Id) + 1);
                            router.RoutingTable.Add(new RoutingEntry 
                            { 
                                Metric = path.TotalCost,
                                TargetIPAdress = path.TargetNode.Value.IPAdress,
                                NetworkInterface = router.NetworkInterfaces.Single(n => n.ConnectedNetworkDevice?.Id == nextHop.Value.Id),
                                NextHop = nextHop.Value.IPAdress,
                                IsActive = true,
                            });
                        }
                    }
                }
            }
        }
    }
}
