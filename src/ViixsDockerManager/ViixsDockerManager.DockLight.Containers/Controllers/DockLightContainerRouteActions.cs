using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ViixsDockerManager.DockLight.Models.Commands;
using ViixsDockerManager.DockLight.Models.Details;
using ViixsDockerManager.DockLight.Models.Queries;
using ViixsDockerManager.Mediator;

namespace ViixsDockerManager.DockLight.Controllers;

internal static class DockLightContainerRouteActions
{
    public static RouteGroupBuilder MapDockLightContainerRouteActions(this RouteGroupBuilder builder)
    {
        builder.MapGet("/", GetContainerDetails);
        builder.MapPost("/start", StartContainer);
        //builder.MapPost("/stop", StartContainer);
        //builder.MapPost("/restart", StartContainer);
        //builder.MapPost("/kill", StartContainer);
        return builder;
    }

    private static Task<ContainerDetails> GetContainerDetails(
    Guid environmentId,
    string containerId,
    IMediator mediator)
    {
        return mediator.Send(new GetContainerDetailsQuery(environmentId, containerId));
    }

    private static Task StartContainer([FromBody] StartContainerCommand startContainerCommand, IMediator mediator)
    {
        return mediator.Send(startContainerCommand);
    }
}
