using Project_RMT.Models;
using System.Net;

namespace Project_RMT.Interfaces
{
    public interface INetworkDevice
    {
        short Id { get; set; }
        IPAddress IPAdress { get; set; }
        List<NetworkInterface> NetworkInterfaces { get; set; }
    }
}