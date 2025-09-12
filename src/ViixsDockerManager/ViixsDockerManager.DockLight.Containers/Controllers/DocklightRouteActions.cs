using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using ViixsDockerManager.DockLight.Models.Details;
using ViixsDockerManager.DockLight.Models.Overview;
using ViixsDockerManager.DockLight.Models.Queries;
using ViixsDockerManager.DockLight.Services.Interfaces;
using ViixsDockerManager.Mediator;

namespace ViixsDockerManager.DockLight.Controllers;

public static class DocklightRouteActions
{
    public static RouteGroupBuilder MapDocklightRouteActions(this RouteGroupBuilder builder)
    {
        builder.MapGet("/", GetAllContainers);
        builder.MapGet("/{containerId}", GetContainerDetails);
        return builder;
    }

    private static Task<IEnumerable<ContainerSummary>> GetAllContainers(
        Guid environmentId,
        IMediator mediator)
    {
        return mediator.Send(new GetContainersForEnvironmentQuery(environmentId));
    }

    private static Task<ContainerDetails> GetContainerDetails(
        Guid environmentId,
        string containerId,
        IMediator mediator)
    {
        return mediator.Send(new GetContainerDetailsQuery(environmentId, containerId));
    }
}
