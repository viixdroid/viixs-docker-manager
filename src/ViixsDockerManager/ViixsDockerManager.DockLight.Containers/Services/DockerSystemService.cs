using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Docker.DotNet;
using ViixsDockerManager.DockLight.Services.Interfaces;

namespace ViixsDockerManager.DockLight.Services;

internal class DockerSystemService : IDockerSystemService
{
    private ISystemOperations _system;

    public DockerSystemService(ISystemOperations system) => _system = system;

    public Task<string> GetDockerVersionAsync(CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public Task<string> GetSystemInfoAsync(CancellationToken cancellationToken = default) => throw new NotImplementedException();
}
