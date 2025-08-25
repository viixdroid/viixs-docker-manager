using Docker.DotNet;

namespace ViixsDockerManager.DockLight.Services.Interfaces;

public interface IDockerClientService
{
    IDockerClient? GetDockerClient();
}
