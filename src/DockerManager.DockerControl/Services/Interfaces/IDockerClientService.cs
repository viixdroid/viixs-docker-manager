using Docker.DotNet;

namespace DockerManager.DockerControl.Services.Interfaces;

public interface IDockerClientService
{
    IDockerClient? GetDockerClient();
}