using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Docker.DotNet;
using ViixsDockerManager.DockLight.Models.Details;
using ViixsDockerManager.DockLight.Models.Overview;
using ViixsDockerManager.DockLight.Services.Interfaces;
using ViixsDockerManager.DockLight.Shared.Exceptions;

namespace ViixsDockerManager.DockLight.Services;

internal class DockerContainerService(IContainerOperations containerOperations) : IDockerContainerService
{
    public async Task<IEnumerable<ContainerSummary>> GetContainerListAsync() //Todo: add filters
    {
        var containers = await containerOperations.ListContainersAsync(new Docker.DotNet.Models.ContainersListParameters { All = true });
        if (containers.Count == 0)
        {
            throw new NoContainersFoundException();
        }
        return containers.Select(c => (ContainerSummary)c);
    }

    public async Task<ContainerDetails> GetContainerDetailAsync(string containerId)
    {
        var container = await containerOperations.InspectContainerAsync(containerId);
        return container == null
            ? throw new ContainerNotFoundException(containerId)
            : (ContainerDetails)container;
    }
}
