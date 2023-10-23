using Project_RMT.Models;
using System.Net;

namespace Project_RMT.Interfaces
{
    public interface INetworkDevice
    {
        Guid Id { get; set; }
        IPAddress IPAdress { get; set; }
        List<NetworkInterface> NetworkInterfaces { get; set; }
    }
}