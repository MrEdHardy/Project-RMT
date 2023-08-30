using Project_RMT.Models;
using System.Net;

namespace Project_RMT.Interfaces
{
    public interface INetworkDevices
    {
        IPAddress IPAdress { get; set; }
        List<NetworkInterface> Ports { get; set; }
    }
}