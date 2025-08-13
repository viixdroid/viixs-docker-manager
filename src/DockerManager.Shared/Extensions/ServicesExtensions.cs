using DockerManager.Shared.Services;
using DockerManager.Shared.Services.Docker;
using DockerManager.Shared.Services.Docker.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DockerManager.Shared.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddSharedServices(this IServiceCollection services)
    {
        // Register shared services here
        services.AddSingleton<INavigationMenuService, NavigationMenuService>();
        services.AddSingleton<IDockerClientService, DockerClientService>();
        services.AddSingleton<IDockerService, DockerService>();

        return services;
    }
}