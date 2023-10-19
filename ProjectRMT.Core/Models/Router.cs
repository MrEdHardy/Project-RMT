using Project_RMT.Interfaces;
using System.Net;

namespace Project_RMT.Models
{
    public record Router : INetworkDevice
    {
        public required short RouterId { get; set; } 
        public required IPAddress IPAdress { get; set; }
        public required List<NetworkInterface> NetworkInterfaces { get; set; }

        public required List<Client> Clients { get; set; }

        public required List<RoutingEntry> RoutingTable { get; set; }
    }
}
