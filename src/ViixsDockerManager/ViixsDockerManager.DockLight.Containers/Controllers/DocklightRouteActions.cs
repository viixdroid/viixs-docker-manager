using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using ViixsDockerManager.DockLight.Services.Interfaces;
using ViixsDockerManager.DockLight.Shared.Models;

namespace ViixsDockerManager.DockLight.Controllers;

public static class DocklightRouteActions
{
    public static RouteGroupBuilder MapDocklightRouteActions(this RouteGroupBuilder builder)
    {
        builder.MapGet("/", GetAllContainers);
        return builder;
    }

    private static async Task<IEnumerable<ContainerSummary>> GetAllContainers(
        Guid environmentId,
        ILoggerFactory loggerFactory,
        IDockerContainersService dockerContainersService)
    {
        var logger = loggerFactory.CreateLogger(nameof(DocklightRouteActions));
        logger.LogInformation("EnvironmentId: {EnvironmentId}", environmentId);
        var containers = await dockerContainersService.GetContainerListAsync(environmentId);
        return containers;
    }
}
