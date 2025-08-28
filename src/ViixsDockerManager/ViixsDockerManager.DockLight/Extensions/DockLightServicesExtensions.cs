using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
}
