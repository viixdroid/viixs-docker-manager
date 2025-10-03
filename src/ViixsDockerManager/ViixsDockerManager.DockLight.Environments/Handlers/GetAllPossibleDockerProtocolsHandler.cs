using Microsoft.Extensions.Configuration;
using ViixsDockerManager.DockLight.Environments.Factories;
using ViixsDockerManager.DockLight.Environments.Helpers;
using ViixsDockerManager.Mediator.Queries;
using ViixsDockerManager.Shared.Models.Dtos.DockLightEnvironments;
using ViixsDockerManager.Shared.Models.Queries.DockLightEnvironments;
using static ViixsDockerManager.DockLight.Shared.Constants.DockLightConstants;


namespace ViixsDockerManager.DockLight.Environments.Handlers;

public class GetAllPossibleDockerProtocolsHandler(IConfiguration configuration) : IQueryHandler<GetPossibleDockerProtocolsQuery, InitialDockerEnvironment>
{
    public Task<InitialDockerEnvironment> Execute(GetPossibleDockerProtocolsQuery query, CancellationToken cancellationToken = default)
    {
        var isRunningInDocker = configuration.GetValue<bool>(DotnetRunningInContainer);
        var runningOsPlatformName = OperatingSystemHelpers.GetPlatformName();

        var dockerProtocol = DockerProtocolFactory.GetDockerProtocol();

        var initialDockerEnvironment = new InitialDockerEnvironment(runningOsPlatformName, isRunningInDocker, dockerProtocol);
        return Task.FromResult(initialDockerEnvironment);
    }
}
