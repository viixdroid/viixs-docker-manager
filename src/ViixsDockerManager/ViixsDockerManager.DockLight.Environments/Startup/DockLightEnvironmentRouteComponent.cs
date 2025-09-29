using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using ViixsDockerManager.DockLight.Environments.Controllers;
using ViixsDockerManager.Shared.AspNet.Startup;

namespace ViixsDockerManager.DockLight.Environments.Startup;

public sealed class DockLightEnvironmentRouteComponent : RouteComponent
{
    private RouteGroupBuilder MapDocklightEnvironmentRoutes(IEndpointRouteBuilder serviceHost)
    {
        var group = serviceHost.MapGroup("docklightenvironments");

        group.MapRouteActions();
        return group;
    }

    private RouteGroupBuilder MapDockLightEnvironmentSetupRoutes(IEndpointRouteBuilder serviceHost)
    {
        var group = serviceHost.MapGroup("setup");

        group.MapDockLightEnvironmentSetupRouteActions();
        return group;
    }


    protected override void ConfigureWebApiRoutes(IEndpointRouteBuilder routeGroupBuilder)
    {
        var group = MapDocklightEnvironmentRoutes(routeGroupBuilder);
        MapDockLightEnvironmentSetupRoutes(group);
    }
}
