using Project_RMT.Interfaces;

namespace Project_RMT.Models
{
    public record NetworkInterface
    {
        public required string Name { get; set; }

        public INetworkDevice? ConnectedNetworkDevice { get; set; }
    }
}