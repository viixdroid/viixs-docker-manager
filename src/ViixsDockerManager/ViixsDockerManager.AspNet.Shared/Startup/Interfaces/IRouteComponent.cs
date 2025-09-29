using Microsoft.AspNetCore.Routing;
using ViixsDockerManager.Shared.Startup.Interfaces;

namespace ViixsDockerManager.Shared.AspNet.Startup.Interfaces;

public interface IRouteComponent : IApplicationBuilderComponent
{
    void ConfigureWebSocketRoutes(IEndpointRouteBuilder webSocketRouteGroupBuilder);
    void ConfigureWebApiRoutes(IEndpointRouteBuilder routeGroupBuilder);
}
