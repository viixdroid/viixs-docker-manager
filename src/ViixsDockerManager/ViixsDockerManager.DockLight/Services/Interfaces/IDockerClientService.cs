using Docker.DotNet;
using ViixsDockerManager.Shared.Services;

namespace ViixsDockerManager.DockLight.Services.Interfaces;

public interface IDockerClientService : IViixsBaseService
{
    IDockerClient? GetDockerClient();
}
