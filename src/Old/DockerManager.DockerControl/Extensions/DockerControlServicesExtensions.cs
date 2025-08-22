using DockerManager.DockerControl.Services;
using DockerManager.DockerControl.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DockerManager.DockerControl.Extensions;

public static class DockerControlServicesExtensions
{
    public static IServiceCollection AddDockerControlServices(this IServiceCollection services)
    {
        // Register Docker control services here
        services.AddSingleton<IDockerClientService, DockerClientService>();
        services.AddSingleton<IDockerService, DockerService>();

        return services;
    }
}
