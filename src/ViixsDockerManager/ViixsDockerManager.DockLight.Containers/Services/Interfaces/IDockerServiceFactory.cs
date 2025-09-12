namespace ViixsDockerManager.DockLight.Services.Interfaces;

internal interface IDockerServiceFactory
{
    Task<TRequestedService> GetServiceAsync<TRequestedService>(Guid environmentId) where TRequestedService : class, IDockerService;
}
