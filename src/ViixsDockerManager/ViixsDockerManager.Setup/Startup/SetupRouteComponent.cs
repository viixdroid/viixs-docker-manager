using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using ViixsDockerManager.Setup.Controllers;
using ViixsDockerManager.Setup.Controllers.Hubs;
using ViixsDockerManager.Shared.AspNet.Startup;

namespace ViixsDockerManager.Setup.Startup;

public class SetupRouteComponent : RouteComponent
{
    protected override void ConfigureWebSocketRoutes(IEndpointRouteBuilder routeGroupBuilder)
    {
        routeGroupBuilder.MapHub<SetupInformationHub>("setup");
    }

    protected override void ConfigureWebApiRoutes(IEndpointRouteBuilder routeGroupBuilder)
    {
        var group = routeGroupBuilder.MapGroup("setup");

        group.MapSetupRouteActions();
    }
}
