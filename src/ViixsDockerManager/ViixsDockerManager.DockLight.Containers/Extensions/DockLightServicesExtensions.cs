using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ViixsDockerManager.DockLight.Controllers;
using ViixsDockerManager.DockLight.Services;
using ViixsDockerManager.DockLight.Services.Interfaces;
using ViixsDockerManager.Shared.Extensions;

namespace ViixsDockerManager.DockLight.Extensions;

public static class DockLightServicesExtensions
{
    public static IServiceCollection AddDockLightServices(this IServiceCollection services)
    {
        services.AddDecoration<IDockerClientService, DockerClientService>();
        services.AddDecoration<IDockerContainersService, DockerContainersService>();

        return services;
    }

    public static RouteGroupBuilder MapDocklightRoutes(this IEndpointRouteBuilder serviceHost)
    {
        var group = serviceHost.MapGroup("{environmentId}/containers");

        group.MapDocklightRouteActions();
        return group;
    }
}
