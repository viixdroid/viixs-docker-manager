using System.Net;
using Docker.DotNet.Models;
using static System.Net.Sockets.AddressFamily;

namespace ViixsDockerManager.DockLight.Models.Overview;

public record ContainerPort(string Ip, ushort PrivatePort, ushort PublicPort, string Type)
{
    public string GetFormattedPort()
    {
        return $"{PublicPort}:{PrivatePort}";
    }

    public bool IsIpV6()
    {
        if (!IPAddress.TryParse(Ip, out var address))
        {
            return false;
        }

        return address.AddressFamily == InterNetworkV6;
    }

    public static implicit operator ContainerPort(Port port)
    {
        return new ContainerPort(port.IP, port.PrivatePort, port.PublicPort, port.Type);
    }
}
