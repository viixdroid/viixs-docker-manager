namespace ViixsDockerManager.DockLight.Services.Interfaces;

internal interface IDockerServiceRunner
{
    Task<TRequestedService> GetServiceAsync<TRequestedService>(Guid environmentId) where TRequestedService : class, IDockerService;
}