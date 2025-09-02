using Docker.DotNet;
using ViixsDockerManager.Shared.Services;

namespace ViixsDockerManager.DockLight.Shared.Services.Interfaces;

public interface IDockerClientService : IViixsBaseService
{
    IDockerClient? GetDockerClient(string? address);
}
