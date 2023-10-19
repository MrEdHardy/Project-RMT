namespace Project_RMT.Collections
{
    public class Edge<T>
        where T : notnull
    {
        public Node<T> Target { get; }
        public int Weight { get; }

        public Edge(Node<T> target, int weight)
        {
            Target = target;
            Weight = weight;
        }
    }
}
