using ViixsDockerManager.DockLight.Services.Interfaces;

namespace ViixsDockerManager.DockLight.Helpers;

internal static class DockerServiceRunnerExtensions
{
    public static Task<IDockerContainerService> GetDockerContainerService(this IDockerServiceRunner dockerServiceRunner, Guid environmentId, CancellationToken cancellationToken = default)
        => dockerServiceRunner.GetServiceAsync<IDockerContainerService>(environmentId);
}
