using Project_RMT.Collections.Internal;
using System.Xml.Linq;

namespace Project_RMT.Collections
{
    public class Graph<T>
        where T : notnull
    {
        public ICollection<Node<T>> Nodes { get; set; }

        public Graph()
        {
            this.Nodes = new List<Node<T>>();
        }

        public Graph(ICollection<Node<T>> nodes)
        {
            this.Nodes = nodes;
        }

        public List<(Node<T> TargetNode, List<Node<T>> Path, int TotalCost)> FindShortestPath(Node<T> source)
        {
            var distances = new Dictionary<Node<T>, int>();
            var visited = new HashSet<Node<T>>();
            var predecessors = new Dictionary<Node<T>, Node<T>>();
            var priorityQueue = new PriorityQueue<(Node<T> node, int distance), int>(new DistanceComparer());

            distances[source] = 0;
            priorityQueue.Enqueue((source, 0), 0);

            while (priorityQueue.Count > 0)
            {
                var (currentNode, currentDistance) = priorityQueue.Dequeue();
                if (visited.Contains(currentNode)) continue;

                visited.Add(currentNode);
                foreach (var edge in currentNode.Edges)
                {
                    var targetNode = edge.Target;
                    var newDistance = currentDistance + edge.Weight;
                    if (!distances.ContainsKey(targetNode) || newDistance < distances[targetNode])
                    {
                        distances[targetNode] = newDistance;
                        predecessors[targetNode] = currentNode;
                        priorityQueue.Enqueue((targetNode, newDistance), newDistance);
                    }
                }
            }

            var result = new List<(Node<T> TargetNode, List<Node<T>> Path, int TotalCost)>();
            foreach (var node in distances.Keys)
            {
                var path = new List<Node<T>>();
                for (var current = node; current != null; current = predecessors.GetValueOrDefault(current))
                {
                    path.Add(current);
                }
                path.Reverse();
                result.Add((node, path, distances[node]));
            }

            return result;
        }
    }
}
