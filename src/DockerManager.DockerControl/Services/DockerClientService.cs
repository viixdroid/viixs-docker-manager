using Docker.DotNet;
using DockerManager.DockerControl.Models;
using DockerManager.DockerControl.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using static DockerManager.DockerControl.Constants.DockerConstants;

namespace DockerManager.DockerControl.Services;

internal class DockerClientService(IConfiguration configuration) : IDockerClientService
{
    public IDockerClient? GetDockerClient()
    {
        var isRunningInDocker = configuration.GetValue<bool>(DotnetRunningInContainer);
        if (!isRunningInDocker)
        {
            //When not giving any information, the client itself can figure out the protocol.
            return CreateDockerClient();
        }

        var unixProtocol = DockerCommunicationProtocol.UnixCommunication();

        if (unixProtocol.AddressExists())
        {
            return CreateDockerClient(unixProtocol.GetProtocolUri());
        }

        return null;
    }

    //TODO: Add logging.
    private static DockerClient CreateDockerClient(Uri? dockerUri = null)
    {
        var dockerClientConfig = new DockerClientConfiguration();
        if (dockerUri is not null)
        {
            dockerClientConfig = new DockerClientConfiguration(dockerUri);
        }

        return dockerClientConfig.CreateClient();
    }
}
