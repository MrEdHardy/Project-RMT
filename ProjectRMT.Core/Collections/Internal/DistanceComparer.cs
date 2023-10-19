namespace Project_RMT.Collections.Internal
{
    public class DistanceComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return x.CompareTo(y);
        }
    }
}
