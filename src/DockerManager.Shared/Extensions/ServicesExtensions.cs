using DockerManager.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DockerManager.Shared.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddSharedServices(this IServiceCollection services)
    {
        // Register shared services here
        services.AddSingleton<INavigationMenuService, NavigationMenuService>();

        return services;
    }
}
