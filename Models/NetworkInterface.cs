using Project_RMT.Interfaces;

namespace Project_RMT.Models
{
    public class NetworkInterface
    {
        public required string Name { get; set; }

        public INetworkDevice? ConnectedNetworkDevice { get; set; }
    }
}