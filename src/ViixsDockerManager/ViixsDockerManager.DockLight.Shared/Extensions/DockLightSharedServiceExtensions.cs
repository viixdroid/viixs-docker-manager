using Microsoft.Extensions.DependencyInjection;
using ViixsDockerManager.DockLight.Shared.Decorators.Providers;

namespace ViixsDockerManager.DockLight.Shared.Extensions;

public static class DockLightSharedServiceExtensions
{
    public static IServiceCollection AddDockLightSharedServices(this IServiceCollection services)
    {
        services.AddScoped<IDockerClientEndpointProvider, DockerClientEndpointProvider>();
        return services;
    }
}
