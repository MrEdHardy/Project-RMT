namespace Project_RMT.Collections
{
    public class TreeNode<T> 
        where T : notnull
    {
        public T Value { get; set; }
        public List<Edge<T>> Edges { get; private set; }

        public TreeNode(T value)
        {
            Value = value;
            Edges = new List<Edge<T>>();
        }

        public void AddEdge(TreeNode<T> targetNode, int weight)
        {
            Edges.Add(new Edge<T>(this, targetNode, weight));
        }
    }
}
