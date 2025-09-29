using Microsoft.AspNetCore.Routing;
using ViixsDockerManager.Shared.AspNet.Startup.Interfaces;

namespace ViixsDockerManager.Shared.AspNet.Startup;

public abstract class RouteComponent : IRouteComponent
{
    protected virtual void ConfigureWebSocketRoutes(IEndpointRouteBuilder routeGroupBuilder) { }

    protected abstract void ConfigureWebApiRoutes(IEndpointRouteBuilder routeGroupBuilder);

    void IRouteComponent.ConfigureWebApiRoutes(IEndpointRouteBuilder routeGroupBuilder) => ConfigureWebApiRoutes(routeGroupBuilder);
    void IRouteComponent.ConfigureWebSocketRoutes(IEndpointRouteBuilder webSocketRouteGroupBuilder) =>  ConfigureWebSocketRoutes(webSocketRouteGroupBuilder);
}
