namespace ViixsDockerManager.DockLight.Services.Interfaces;

internal interface IDockerImageService : IDockerService
{
    public Task<IEnumerable<string>> GetImageListAsync(CancellationToken cancellationToken = default);

    public Task<string> GetImageDetailsAsync(string imageId, CancellationToken cancellationToken = default);

    public Task DeleteImageAsync(string imageId,  CancellationToken cancellationToken = default);
}
