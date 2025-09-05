using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using ViixsDockerManager.DockLight.Environments.Models.Commands;
using ViixsDockerManager.DockLight.Environments.Models.Queries;
using ViixsDockerManager.DockLight.Shared.Entities;
using ViixsDockerManager.Mediator;
using ViixsDockerManager.Mediator.Queries;
using ViixsDockerManager.Shared.Database.Repositories;

namespace ViixsDockerManager.DockLight.Environments.Controllers;

public static class DocklightEnvironmentsRouteActions
{
    public static RouteGroupBuilder MapRouteActions(this RouteGroupBuilder builder)
    {
        builder.MapGet("/", GetAllDockLightEnvironments);
        builder.MapPost("/", CreateDockLightEnvironment);
        return builder;
    }

    private static async Task<IEnumerable<DockLightEnvironment>> GetAllDockLightEnvironments([FromServices] IMediator mediator) //, [FromQuery] DockLightFilter filter)
    {
        var result = await mediator.Send(new GetAllDockLightEnvironmentsQuery());
        return result;
    }

    private static async Task CreateDockLightEnvironment([FromBody] CreateDockLightEnvironmentCommand createDockLightEnvironmentCommand, [FromServices] IMediator mediator)
    {
        await mediator.Send(createDockLightEnvironmentCommand).ConfigureAwait(false);
    }
}
