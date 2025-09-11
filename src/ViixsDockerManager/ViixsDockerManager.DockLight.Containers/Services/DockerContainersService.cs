using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.Extensions.Logging;
using ViixsDockerManager.DockLight.Services.Interfaces;
using ViixsDockerManager.DockLight.Shared.Entities;
using ViixsDockerManager.DockLight.Shared.Exceptions;
using ViixsDockerManager.DockLight.Shared.Models;
using ViixsDockerManager.DockLight.Shared.Queries.Filters;
using ViixsDockerManager.DockLight.Shared.Services.Interfaces;
using ViixsDockerManager.Shared.Database.Repositories;
using ViixsDockerManager.Shared.Exceptions;
using ViixsDockerManager.Shared.Helpers;

namespace ViixsDockerManager.DockLight.Services;

public class DockerContainersService(ILogger<DockerContainersService> logger) : IDockerContainersService
{

    public async Task<IEnumerable<ContainerSummary>> GetContainerListAsync(Guid environmentId, IContainerOperations? containerOperations = null)
    {
        containerOperations = Guard.ValueIsNotNull(containerOperations, nameof(containerOperations));

        //TODO: Actually add some filters.
        var containerListParameters = new ContainersListParameters { All = true };
        logger.LogInformation("Getting Containers");
        try
        {

            var containers = await containerOperations.ListContainersAsync(containerListParameters);
            logger.LogInformation("We gotten containers? {Containers}", string.Join(',', containers.Select(c => c.Names[0])));
            if (containers.Count == 0)
            {
                logger.LogInformation("We have not containers");
                throw new NoContainersFoundException();
            }


            return containers.Select(c => (ContainerSummary)c);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error getting containers");
            throw;
        }
    }
}
