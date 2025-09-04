using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using ViixsDockerManager.DockLight.Shared.Entities;
using ViixsDockerManager.DockLight.Shared.Models.Commands;
using ViixsDockerManager.DockLight.Shared.Models.Queries;
using ViixsDockerManager.Mediator;
using ViixsDockerManager.Mediator.Queries;
using ViixsDockerManager.Shared.Database.Repositories;

namespace ViixsDockerManager.DockLight.Environments.Controllers;

public static class DocklightEnvironmentsRouteActions
{
    public static RouteGroupBuilder MapRouteActions(this RouteGroupBuilder builder)
    {
        builder.MapGet("/", GetAllDockLightEnvironments);
        builder.MapGet("/mediator", GetAllDockLightEnvironmentsWithMediator);
        builder.MapPost("/mediator", CreateDockLightEnvironment);
        return builder;
    }

    private static async Task<IResult> GetAllDockLightEnvironments(IReadRepository<DockLightEnvironment> readRepository)
    {
        var environments = await readRepository.GetAllAsync();
        return Results.Ok(environments);
    }

    private static async Task<IEnumerable<DockLightEnvironment>> GetAllDockLightEnvironmentsWithMediator([FromServices] IMediator mediator) //, [FromQuery] DockLightFilter filter)
    {
        var result = await mediator.Send(new GetAllDockLightEnvironmentsQuery());
        return result;
    }

    private static async Task CreateDockLightEnvironment([FromBody] CreateDockLightEnvironmentCommand createDockLightEnvironmentCommand, [FromServices] IMediator mediator)
    {
        await mediator.Send(createDockLightEnvironmentCommand).ConfigureAwait(false);
    }
}
