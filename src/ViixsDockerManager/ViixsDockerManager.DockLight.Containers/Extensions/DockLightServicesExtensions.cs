using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ViixsDockerManager.DockLight.Controllers;
using ViixsDockerManager.DockLight.Services;
using ViixsDockerManager.DockLight.Services.Interfaces;
using ViixsDockerManager.DockLight.Shared.Decorators;
using ViixsDockerManager.DockLight.Shared.Decorators.Providers;
using ViixsDockerManager.DockLight.Shared.Entities;
using ViixsDockerManager.DockLight.Shared.Services;
using ViixsDockerManager.DockLight.Shared.Services.Interfaces;
using ViixsDockerManager.Shared.Database.Repositories;
using ViixsDockerManager.Shared.Extensions;

namespace ViixsDockerManager.DockLight.Extensions;

public static class DockLightServicesExtensions
{
    public static IServiceCollection AddDockLightServices(this IServiceCollection services)
    {
        services.AddDecoration<IDockerClientService, DockerClientService>();
        services.AddDecoration<IDockerContainersService, DockerContainersService>((serviceToDecorate, serviceProvider) => DockerClientDecorator<IDockerContainersService>
            .CreateService(
                serviceToDecorate,
                serviceProvider.GetRequiredService<IDockerClientEndpointProvider>())
        );

        return services;
    }

    public static RouteGroupBuilder MapDocklightRoutes(this IEndpointRouteBuilder serviceHost)
    {
        var group = serviceHost.MapGroup("{environmentId:guid}/containers");

        group.MapDocklightRouteActions();
        return group;
    }
}
