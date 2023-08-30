using System.Net;

namespace Project_RMT.Models
{
    public class RoutingEntries
    {
        public required IPAddress Network { get; set; }

        public IPAddress? NextHop { get; set; }

        public required int Port { get; set; }

        public required int Metric { get; set; }

        public bool IsActive { get; set; } = false;
    }
}