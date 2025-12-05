using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using ViixsDockerManager.Shared.AspNet.Startup;
using ViixsDockerManager.Version.Controllers;

namespace ViixsDockerManager.Version.Startup;

public sealed class VersionRouteComponent : RouteComponent
{
    protected override void ConfigureWebApiRoutes(IEndpointRouteBuilder routeGroupBuilder)
    {
        var versionGroup = routeGroupBuilder.MapGroup("/version");

        versionGroup.MapVersionRouteActions();
    }
}
