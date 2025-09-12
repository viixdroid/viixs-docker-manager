using ViixsDockerManager.DockLight.Services.Interfaces;

namespace ViixsDockerManager.DockLight.Helpers;

internal static class DockerServiceRunnerExtensions
{
    public static Task<IDockerContainerService> GetDockerContainerService(this IDockerServiceFactory dockerServiceRunner, Guid environmentId, CancellationToken cancellationToken = default)
        => dockerServiceRunner.GetServiceAsync<IDockerContainerService>(environmentId);

    public static Task<IDockerImageService> GetDockerImageService(this IDockerServiceFactory dockerServiceRunner, Guid environmentId, CancellationToken cancellationToken = default)
    => dockerServiceRunner.GetServiceAsync<IDockerImageService>(environmentId);

    public static Task<IDockerSystemService> GetDockerSystemService(this IDockerServiceFactory dockerServiceRunner, Guid environmentId, CancellationToken cancellationToken = default)
    => dockerServiceRunner.GetServiceAsync<IDockerSystemService>(environmentId);
}
