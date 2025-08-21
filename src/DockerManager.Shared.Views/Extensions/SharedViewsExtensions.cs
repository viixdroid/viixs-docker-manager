using Microsoft.Extensions.DependencyInjection;

namespace DockerManager.Shared.Views.Extensions;
public static class SharedViewsExtensions
{
    public static IServiceCollection AddDockerManagerSharedViewServices(this IServiceCollection services)
    {
        // Register shared view services here
        services.AddScoped<Services.Interfaces.IAsyncServicesFactory, Services.AsyncServicesFactory>();
        services.AddScoped<Services.Interfaces.ILocalStorageService, Services.LocalStorageService>();
        services.AddScoped<Services.Interfaces.IThemeService, Services.ThemeService>();

        return services;
    }
}
