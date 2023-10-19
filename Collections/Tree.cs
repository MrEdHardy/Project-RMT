using Project_RMT.Collections.Internal;

namespace Project_RMT.Collections
{
    public class Tree<T>
        where T : notnull
    {
        public TreeNode<T> Root { get; set; }

        public Tree(T rootValue)
        {
            Root = new TreeNode<T>(rootValue);
        }

        public Dictionary<TreeNode<T>, int> FindShortestPathFromRoot()
        {
            var distances = new Dictionary<TreeNode<T>, int>();
            var visited = new HashSet<TreeNode<T>>();
            var priorityQueue = new PriorityQueue<(TreeNode<T> node, int distance), int>(new DistanceComparer());

            distances[this.Root] = 0;
            priorityQueue.Enqueue((this.Root, 0), 0);

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
                        priorityQueue.Enqueue((targetNode, newDistance), newDistance);
                    }
                }
            }

            return distances;
        }
    }
}
