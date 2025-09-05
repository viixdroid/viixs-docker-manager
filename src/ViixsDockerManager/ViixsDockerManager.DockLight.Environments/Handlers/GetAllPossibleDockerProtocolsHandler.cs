using System.Runtime.InteropServices;
using Microsoft.Extensions.Configuration;
using ViixsDockerManager.DockLight.Environments.Models.Dtos;
using ViixsDockerManager.DockLight.Environments.Models.Queries;
using ViixsDockerManager.DockLight.Shared.Factories;
using ViixsDockerManager.Mediator.Queries;
using static ViixsDockerManager.DockLight.Shared.Constants.DockLightConstants;


namespace ViixsDockerManager.DockLight.Environments.Handlers;

public class GetAllPossibleDockerProtocolsHandler(IConfiguration configuration) : IQueryHandler<GetPossibleDockerProtocolsQuery, InitialDockerEnvironment>
{
    public Task<InitialDockerEnvironment> Execute(GetPossibleDockerProtocolsQuery query, CancellationToken cancellationToken = default)
    {
        var isRunningInDocker = configuration.GetValue<bool>(DotnetRunningInContainer);
        var runningOsDescription = RuntimeInformation.OSDescription;

        var communicationProtocol = DockerCommunicationProtocolFactory.GetCommunicationProtocol();
        var dockerProtocol = DockerProtocol.FromDockerCommunicationProtocol(communicationProtocol);

        var initialDockerEnvironment = new InitialDockerEnvironment(runningOsDescription, isRunningInDocker, dockerProtocol);
        return Task.FromResult(initialDockerEnvironment);
    }
}
