using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ViixsDockerManager.DockLight.Controllers;
using ViixsDockerManager.DockLight.Handlers;
using ViixsDockerManager.DockLight.Models.Queries;
using ViixsDockerManager.DockLight.Services;
using ViixsDockerManager.DockLight.Services.Interfaces;
using ViixsDockerManager.DockLight.Services.Runners;
using ViixsDockerManager.DockLight.Shared.Decorators;
using ViixsDockerManager.DockLight.Shared.Decorators.Providers;
using ViixsDockerManager.DockLight.Shared.Entities;
using ViixsDockerManager.DockLight.Shared.Services;
using ViixsDockerManager.DockLight.Shared.Services.Interfaces;
using ViixsDockerManager.Mediator.Extensions;
using ViixsDockerManager.Shared.Database.Repositories;
using ViixsDockerManager.Shared.Extensions;

namespace ViixsDockerManager.DockLight.Extensions;

public static class DockLightServicesExtensions
{
    public static IServiceCollection AddDockLightContainerServices(this IServiceCollection services)
    {
        services.AddDecoration<IDockerClientService, DockerClientService>();
        services.AddScoped<IDockerServiceRunner, DockLightServiceRunner>();

        services.RegisterQueryHandler<GetContainersForEnvironmentHandler, GetContainersForEnvironmentQuery>();
        services.RegisterQueryHandler<GetContainerDetailsHandler, GetContainerDetailsQuery>();

        //services.AddDecoration<IDockerContainersOldService, DockerContainersService>((serviceToDecorate, serviceProvider) => DockerClientDecorator<IDockerContainersOldService>
        //    .CreateService(
        //        serviceToDecorate,
        //        serviceProvider.GetRequiredService<IDockerClientEndpointProvider>())
        //);

        return services;
    }

    public static RouteGroupBuilder MapDocklightRoutes(this IEndpointRouteBuilder serviceHost)
    {
        var group = serviceHost.MapGroup("{environmentId:guid}/containers");

        group.MapDocklightRouteActions();
        return group;
    }
}
