using ViixsDockerManager.DockLight.Models.Queries;
using ViixsDockerManager.DockLight.Services.Interfaces;

namespace ViixsDockerManager.DockLight.Handlers;

internal abstract class DockLightHandlerBase(IDockerServiceFactory dockerServiceFactory)
{
    protected Task<IDockerContainerService> GetDockerContainerService(DockerBaseQuery dockerBaseQuery, CancellationToken cancellationToken = default)
        => dockerServiceFactory.GetServiceAsync<IDockerContainerService>(dockerBaseQuery.EnvironmentId);

    protected Task<IDockerImageService> GetDockerImageService(DockerBaseQuery dockerBaseQuery, CancellationToken cancellationToken = default)
        => dockerServiceFactory.GetServiceAsync<IDockerImageService>(dockerBaseQuery.EnvironmentId);

    protected Task<IDockerSystemService> GetDockerSystemService(DockerBaseQuery dockerBaseQuery, CancellationToken cancellationToken = default)
        => dockerServiceFactory.GetServiceAsync<IDockerSystemService>(dockerBaseQuery.EnvironmentId);
}
