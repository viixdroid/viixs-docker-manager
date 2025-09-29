using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using ViixsDockerManager.DockLight.Controllers;
using ViixsDockerManager.DockLight.Controllers.Hubs;
using ViixsDockerManager.Shared.AspNet.Startup;

namespace ViixsDockerManager.DockLight.Startup;

public sealed class DockLightContainerRouteComponent : RouteComponent
{
    public RouteGroupBuilder MapDocklightRoutes(IEndpointRouteBuilder endPointRouteBuilder)
    {
        var group = endPointRouteBuilder.MapGroup("{environmentId:guid}/containers");

        var dockLightRoutesGroup = group.MapDocklightRouteActions();
        MapDockLightContainerRoutes(dockLightRoutesGroup);
        return group;
    }

    private RouteGroupBuilder MapDockLightContainerRoutes(IEndpointRouteBuilder endPointRouteBuilder)
    {
        var group = endPointRouteBuilder.MapGroup("/{containerId}");
        group.MapDockLightContainerRouteActions();
        return group;
    }
    private void MapDockLightSignalRHubs(IEndpointRouteBuilder endPointRouteBuilder)
    {
        endPointRouteBuilder.MapHub<DockLightInformationHub>("/docklight");
    }

    protected override void ConfigureWebApiRoutes(IEndpointRouteBuilder routeGroupBuilder)
    {
        MapDocklightRoutes(routeGroupBuilder);
    }

    protected override void ConfigureWebSocketRoutes(IEndpointRouteBuilder routeGroupBuilder)
    {
        MapDockLightSignalRHubs(routeGroupBuilder);
    }
}
