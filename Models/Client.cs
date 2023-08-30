using Project_RMT.Interfaces;
using System.Net;

namespace Project_RMT.Models
{
    public record Client : INetworkDevices
    {
        public required List<NetworkInterface> Ports { get; set; }
        public required IPAddress IPAdress { get; set; }
    }
}