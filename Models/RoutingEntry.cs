using System.Net;

namespace Project_RMT.Models
{
    public class RoutingEntry
    {
        public required IPAddress Network { get; set; }

        public IPAddress? NextHop { get; set; }

        public required NetworkInterface NetworkInterface { get; set; }

        public required int Metric { get; set; }

        public bool IsActive { get; set; } = false;
    }
}