using Project_RMT.Interfaces;
using System.Net;

namespace Project_RMT.Models
{
    public record Client : INetworkDevice
    {
        public required List<NetworkInterface> NetworkInterfaces { get; set; }
        public required IPAddress IPAdress { get; set; }
        public required short Id { get; set; }
    }
}