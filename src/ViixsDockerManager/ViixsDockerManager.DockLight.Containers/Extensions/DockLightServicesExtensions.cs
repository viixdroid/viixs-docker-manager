using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using ViixsDockerManager.DockLight.Controllers;
using ViixsDockerManager.DockLight.Controllers.Hubs;
using ViixsDockerManager.DockLight.Handlers;
using ViixsDockerManager.DockLight.Models.Commands;
using ViixsDockerManager.DockLight.Models.Queries;
using ViixsDockerManager.DockLight.Services;
using ViixsDockerManager.DockLight.Services.Interfaces;
using ViixsDockerManager.DockLight.Services.Runners;
using ViixsDockerManager.DockLight.Shared.Services;
using ViixsDockerManager.DockLight.Shared.Services.Interfaces;
using ViixsDockerManager.Mediator.Extensions;
using ViixsDockerManager.Shared.Extensions;

namespace ViixsDockerManager.DockLight.Extensions;

public static class DockLightServicesExtensions
{
    public static IServiceCollection AddDockLightContainerServices(this IServiceCollection services)
    {
        services.AddDecoration<IDockerClientService, DockerClientService>();
        services.AddDecoration<ISendContainerNotifications, SendContainerNotifications>();
        services.AddScoped<IDockerServiceFactory, DockerServiceFactory>();

        services.RegisterQueryHandler<GetContainersForEnvironmentHandler, GetContainersForEnvironmentQuery>();
        services.RegisterQueryHandler<GetContainerDetailsHandler, GetContainerDetailsQuery>();
        services.RegisterCommandHandler<StartContainerHandler, StartContainerCommand>();

        return services;
    }

    public static RouteGroupBuilder MapDocklightRoutes(this IEndpointRouteBuilder endPointRouteBuilder)
    {
        var group = endPointRouteBuilder.MapGroup("{environmentId:guid}/containers");

        var dockLightRoutesGroup = group.MapDocklightRouteActions();
        MapDockLightContainerRoutes(dockLightRoutesGroup);
        return group;
    }

    private static RouteGroupBuilder MapDockLightContainerRoutes(this IEndpointRouteBuilder endPointRouteBuilder)
    {
        var group = endPointRouteBuilder.MapGroup("/{containerId}");
        group.MapDockLightContainerRouteActions();
        return group;
    }

    public static void MapDockLightSignalRHubs(this IEndpointRouteBuilder endPointRouteBuilder)
    {
        endPointRouteBuilder.MapHub<DockLightInformationHub>("/docklight");
    }
}
