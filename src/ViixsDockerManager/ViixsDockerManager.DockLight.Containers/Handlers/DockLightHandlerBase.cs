using ViixsDockerManager.DockLight.Services.Interfaces;

namespace ViixsDockerManager.DockLight.Handlers;

internal abstract class DockLightHandlerBase(IDockerServiceFactory dockerServiceFactory)
{
    protected Task<IDockerContainerService> GetDockerContainerService(Guid environmentId, CancellationToken cancellationToken = default)
        => dockerServiceFactory.GetServiceAsync<IDockerContainerService>(environmentId);

    protected Task<IDockerImageService> GetDockerImageService(Guid environmentId, CancellationToken cancellationToken = default)
        => dockerServiceFactory.GetServiceAsync<IDockerImageService>(environmentId);

    protected Task<IDockerSystemService> GetDockerSystemService(Guid environmentId, CancellationToken cancellationToken = default)
        => dockerServiceFactory.GetServiceAsync<IDockerSystemService>(environmentId);
}
