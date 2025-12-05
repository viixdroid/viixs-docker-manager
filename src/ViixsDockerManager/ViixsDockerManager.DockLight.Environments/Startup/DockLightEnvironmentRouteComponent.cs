using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using ViixsDockerManager.DockLight.Environments.Controllers;
using ViixsDockerManager.DockLight.Controllers;
using ViixsDockerManager.Shared.AspNet.Startup;

namespace ViixsDockerManager.DockLight.Environments.Startup;

public sealed class DockLightEnvironmentRouteComponent : RouteComponent
{
    private static RouteGroupBuilder MapDocklightEnvironmentRoutes(IEndpointRouteBuilder serviceHost)
    {
        var group = serviceHost.MapGroup("docklightenvironments");

        group.MapSetupRoute2RouteActions();
        group.MapRouteActions();
        return group;
    }

    protected override void ConfigureWebApiRoutes(IEndpointRouteBuilder routeGroupBuilder)
    {
        _ = MapDocklightEnvironmentRoutes(routeGroupBuilder);
    }
}
