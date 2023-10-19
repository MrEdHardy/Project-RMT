namespace Project_RMT.Collections
{
    public class Node<T> 
        where T : notnull
    {
        public T Value { get; }
        public ICollection<Edge<T>> Edges { get; set; }

        public Node(T value)
        {
            Value = value;
            Edges = new List<Edge<T>>();
        }
    }
}
