using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using ViixsDockerManager.DockLight.Controllers;
using ViixsDockerManager.DockLight.Handlers;
using ViixsDockerManager.DockLight.Models.Queries;
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
        services.AddScoped<IDockerServiceFactory, DockerServiceFactory>();

        services.RegisterQueryHandler<GetContainersForEnvironmentHandler, GetContainersForEnvironmentQuery>();
        services.RegisterQueryHandler<GetContainerDetailsHandler, GetContainerDetailsQuery>();

        return services;
    }

    public static RouteGroupBuilder MapDocklightRoutes(this IEndpointRouteBuilder serviceHost)
    {
        var group = serviceHost.MapGroup("{environmentId:guid}/containers");

        group.MapDocklightRouteActions();
        return group;
    }
}
