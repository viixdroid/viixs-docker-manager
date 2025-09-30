using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ViixsDockerManager.DockLight.Environments.Models.Dtos;
using ViixsDockerManager.DockLight.Environments.Models.Queries;
using ViixsDockerManager.Mediator;

namespace ViixsDockerManager.DockLight.Environments.Controllers;

internal static class DockLightEnvironmentSetupRouteActions
{
    public static RouteGroupBuilder MapDockLightEnvironmentSetupRouteActions(this RouteGroupBuilder builder)
    {
        builder.MapGet("/protocols", GetDockLightProtocols);
        return builder;
    }

    private static Task<InitialDockerEnvironment> GetDockLightProtocols(IMediator mediator)
    {
        var query = new GetPossibleDockerProtocolsQuery();
        return mediator.Send(query);
    }
}
