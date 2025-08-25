using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ViixsDockerManager.DockLight.Services;
using ViixsDockerManager.DockLight.Services.Interfaces;

namespace ViixsDockerManager.DockLight.Extensions;

public static class DockLightServicesExtensions
{
    public static IServiceCollection AddDockLightServices(this IServiceCollection services)
    {
        services.AddSingleton<IDockerClientService, DockerClientService>();
        services.AddSingleton<IDockerContainersService, DockerContainersService>();

        return services;
    }
}
