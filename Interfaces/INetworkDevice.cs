using Project_RMT.Models;
using System.Net;

namespace Project_RMT.Interfaces
{
    public interface INetworkDevice
    {
        IPAddress IPAdress { get; set; }
        List<NetworkInterface> Ports { get; set; }
    }
}