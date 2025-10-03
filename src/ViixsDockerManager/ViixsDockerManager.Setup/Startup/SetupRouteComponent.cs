using Microsoft.AspNetCore.Routing;
using ViixsDockerManager.Shared.AspNet.Startup;
using ViixsDockerManager.Setup.Shared.Controllers;
using Microsoft.AspNetCore.Builder;

namespace ViixsDockerManager.Setup.Startup;

internal class SetupRouteComponent : RouteComponent
{
    protected override void ConfigureWebApiRoutes(IEndpointRouteBuilder routeGroupBuilder)
    {
        var group = routeGroupBuilder.MapGroup("setup");

        group.MapSetupRouteActions();
    }
}
