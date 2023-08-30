namespace Project_RMT.Models
{
    public class NetworkInterface
    {
        public required string Name { get; set; }

        public Router? ConnectedRouter { get; set; }
    }
}