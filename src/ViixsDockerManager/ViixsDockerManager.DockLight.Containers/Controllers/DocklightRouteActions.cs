using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using ViixsDockerManager.DockLight.Models.Commands;
using ViixsDockerManager.DockLight.Models.Details;
using ViixsDockerManager.DockLight.Models.Overview;
using ViixsDockerManager.DockLight.Models.Queries;
using ViixsDockerManager.Mediator;

namespace ViixsDockerManager.DockLight.Controllers;

public static class DocklightRouteActions
{
    public static RouteGroupBuilder MapDocklightRouteActions(this RouteGroupBuilder builder)
    {
        builder.MapGet("/", GetAllContainers);
        return builder;
    }

    private static Task<IEnumerable<ContainerSummary>> GetAllContainers(
        Guid environmentId,
        IMediator mediator)
    {
        return mediator.Send(new GetContainersForEnvironmentQuery(environmentId));
    }

}
