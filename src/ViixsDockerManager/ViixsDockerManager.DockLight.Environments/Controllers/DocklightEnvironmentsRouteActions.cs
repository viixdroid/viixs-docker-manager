using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using ViixsDockerManager.DockLight.Shared.Entities;
using ViixsDockerManager.Shared.Database.Repositories;

namespace ViixsDockerManager.DockLight.Environments.Controllers;

public static class DocklightEnvironmentsRouteActions
{
    public static RouteGroupBuilder MapRouteActions(this RouteGroupBuilder builder)
    {
        builder.MapGet("/", GetAllDockLightEnvironments);
        return builder;
    }

    private static async Task<IResult> GetAllDockLightEnvironments(IReadRepository<DocklightEnvironment> readRepository)
    {
        var environments = await readRepository.GetAllAsync();
        return Results.Ok(environments);
    }
}
