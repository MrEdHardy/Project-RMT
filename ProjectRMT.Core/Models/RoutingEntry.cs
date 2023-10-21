using System.Net;

namespace Project_RMT.Models
{
    public record RoutingEntry
    {
        public required IPAddress TargetIPAdress { get; set; }

        public IPAddress? NextHop { get; set; }

        public required NetworkInterface NetworkInterface { get; set; }

        public required int Metric { get; set; }

        public bool IsActive { get; set; } = false;
    }
}