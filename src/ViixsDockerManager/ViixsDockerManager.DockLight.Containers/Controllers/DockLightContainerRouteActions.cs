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
        builder.MapPost("/stop", StopContainer);
        builder.MapPost("/restart", RestartContainer);
        builder.MapPost("/kill", KillContainer);
        return builder;
    }

    private static Task<ContainerDetails> GetContainerDetails(Guid environmentId, string containerId, IMediator mediator)
    {
        return mediator.Send(new GetContainerDetailsQuery(environmentId, containerId));
    }

    private static Task StartContainer([FromBody] StartContainerCommand startContainerCommand, IMediator mediator)
    {
        return mediator.Send(startContainerCommand);
    }

    private static Task StopContainer([FromBody] StopContainerCommand stopContainerCommand, IMediator mediator)
    {
        return mediator.Send(stopContainerCommand);
    }

    private static Task RestartContainer([FromBody] RestartContainerCommand restartContainerCommand, IMediator mediator)
    {
        return mediator.Send(restartContainerCommand);
    }

    private static Task KillContainer([FromBody] KillContainerCommand killContainerCommand, IMediator mediator)
    {
        return mediator.Send(killContainerCommand);
    }
}
