namespace ViixsDockerManager.DockLight.Services.Interfaces;

internal interface IDockerSystemService : IDockerService
{
    public Task<string> GetSystemInfoAsync(CancellationToken cancellationToken = default);

    public Task<string> GetDockerVersionAsync(CancellationToken cancellationToken = default);
}
