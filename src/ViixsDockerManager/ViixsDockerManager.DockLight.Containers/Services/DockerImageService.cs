using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Docker.DotNet;
using ViixsDockerManager.DockLight.Services.Interfaces;

namespace ViixsDockerManager.DockLight.Services;

internal class DockerImageService : IDockerImageService
{
    private readonly IImageOperations _images;

    public DockerImageService(IImageOperations images) => _images = images;

    public Task DeleteImageAsync(string imageId, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public Task<string> GetImageDetailsAsync(string imageId, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public Task<IEnumerable<string>> GetImageListAsync(CancellationToken cancellationToken = default) => throw new NotImplementedException();
}
