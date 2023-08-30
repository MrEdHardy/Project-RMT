using Project_RMT.Interfaces;
using System.Net;

namespace Project_RMT.Models
{
    public record Router : INetworkDevice
    {
        public required IPAddress IPAdress { get; set; }
        public required List<NetworkInterface> Ports { get; set; }

        public required List<Client> Clients { get; set; }

        public required List<RoutingEntries> RoutingTable { get; set; }
    }
}
