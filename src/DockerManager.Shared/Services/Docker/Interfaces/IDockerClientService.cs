using Docker.DotNet;

namespace DockerManager.Shared.Services.Docker.Interfaces;

public interface IDockerClientService
{
    IDockerClient? GetDockerClient();
}