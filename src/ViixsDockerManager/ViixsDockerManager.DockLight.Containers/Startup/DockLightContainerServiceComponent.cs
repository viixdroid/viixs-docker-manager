using Microsoft.Extensions.DependencyInjection;
using ViixsDockerManager.DockLight.Handlers;
using ViixsDockerManager.DockLight.Models.Commands;
using ViixsDockerManager.DockLight.Models.Queries;
using ViixsDockerManager.DockLight.Services;
using ViixsDockerManager.DockLight.Services.Interfaces;
using ViixsDockerManager.DockLight.Services.Runners;
using ViixsDockerManager.DockLight.Shared.Services;
using ViixsDockerManager.DockLight.Shared.Services.Interfaces;
using ViixsDockerManager.Mediator.Extensions;
using ViixsDockerManager.Shared.AspNet.Startup;
using ViixsDockerManager.Shared.Extensions;

namespace ViixsDockerManager.DockLight.Startup;

public sealed class DockLightContainerServiceComponent : ServiceComponent
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddDecoration<IDockerClientService, DockerClientService>();
        services.AddDecoration<ISendContainerNotifications, SendContainerNotifications>();
        services.AddScoped<IDockerServiceFactory, DockerServiceFactory>();
    }
    protected override void ConfigureCommandHandlers(IServiceCollection services)
    {
        services.RegisterCommandHandler<StartContainerHandler, StartContainerCommand>();
        services.RegisterCommandHandler<StopContainerHandler, StopContainerCommand>();
        services.RegisterCommandHandler<RestartContainerHandler, RestartContainerCommand>();
        services.RegisterCommandHandler<KillContainerHandler, KillContainerCommand>();
    }

    protected override void ConfigureQueryHandlers(IServiceCollection services)
    {
        services.RegisterQueryHandler<GetContainersForEnvironmentHandler, GetContainersForEnvironmentQuery>();
        services.RegisterQueryHandler<GetContainerDetailsHandler, GetContainerDetailsQuery>();
    }
}
