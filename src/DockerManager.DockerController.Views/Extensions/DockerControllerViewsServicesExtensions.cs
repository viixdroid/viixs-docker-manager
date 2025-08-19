using DockerManager.DockerController.Views.Services;
using DockerManager.DockerController.Views.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DockerManager.DockerController.Views.Extensions;

public static class DockerControllerViewsServicesExtensions
{
    public static IServiceCollection AddDockerControllerViewsServices(this IServiceCollection services)
    {
        services.AddScoped<IContainerService, ContainerService>();
        // services.AddScoped<Services.Interfaces.IImageService, Services.ImageService>();
        // services.AddScoped<Services.Interfaces.INetworkService, Services.NetworkService>();
        // services.AddScoped<Services.Interfaces.IVolumeService, Services.VolumeService>();
        return services;
    }
}
