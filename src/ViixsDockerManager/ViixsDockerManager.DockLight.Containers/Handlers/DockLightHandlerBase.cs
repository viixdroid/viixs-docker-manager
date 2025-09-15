using ViixsDockerManager.DockLight.Models;
using ViixsDockerManager.DockLight.Models.Queries;
using ViixsDockerManager.DockLight.Services.Interfaces;

namespace ViixsDockerManager.DockLight.Handlers;

internal abstract class DockLightHandlerBase(IDockerServiceFactory dockerServiceFactory)
{
    protected Task<IDockerContainerService> GetDockerContainerService(IEnvironmentContext envivronmentContext, CancellationToken cancellationToken = default)
        => dockerServiceFactory.GetServiceAsync<IDockerContainerService>(envivronmentContext.EnvironmentId);

    protected Task<IDockerImageService> GetDockerImageService(IEnvironmentContext envivronmentContext, CancellationToken cancellationToken = default)
        => dockerServiceFactory.GetServiceAsync<IDockerImageService>(envivronmentContext.EnvironmentId);

    protected Task<IDockerSystemService> GetDockerSystemService(IEnvironmentContext envivronmentContext, CancellationToken cancellationToken = default)
        => dockerServiceFactory.GetServiceAsync<IDockerSystemService>(envivronmentContext.EnvironmentId);
}
