namespace Project_RMT.Collections
{
    public class Edge<T>
        where T : notnull
    {
        public TreeNode<T> Source { get; }
        public TreeNode<T> Target { get; }
        public int Weight { get; }

        public Edge(TreeNode<T> source, TreeNode<T> target, int weight)
        {
            Source = source;
            Target = target;
            Weight = weight;
        }
    }
}
