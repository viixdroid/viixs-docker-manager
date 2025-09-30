using ViixsDockerManager.Server.Startup.Factories;
using ViixsDockerManager.Server.Startup.Interfaces;
using ViixsDockerManager.Shared.AspNet.Extensions;
using ViixsDockerManager.Shared.AspNet.Startup.Interfaces;
using ViixsDockerManager.Shared.Helpers;

namespace ViixsDockerManager.Server.Startup;

internal sealed class ConfigureRoutesStartupFeature(IBuilderCollectionFactory builderCollectionFactory) : IConfigureRoutesStartupFeature, IStartupFeature
{
    private const string WebSocketRoute = "ws";
    private const string ApiRoute = "api";

    private void ConfigureRoutes(IEndpointRouteBuilder webApp)
    {
        var routeComponents = builderCollectionFactory.GetBuilderCollection<IRouteComponent>();
        routeComponents = Guard.ValueIsNotNull(routeComponents, nameof(routeComponents));
        var components = routeComponents.GetComponents();
        foreach (var component in components)
        {
            component.ConfigureWebSocketRoutes(CreateDefaultWebSocketRoute(webApp));
            component.ConfigureWebApiRoutes(CreateDefaultApiRoute(webApp));
        }
    }

    private static RouteGroupBuilder CreateDefaultWebSocketRoute(IEndpointRouteBuilder webApp)
        => webApp.MapGroup(WebSocketRoute);

    private static RouteGroupBuilder CreateDefaultApiRoute(IEndpointRouteBuilder webApp)
    {
        var builderWithAppliedEndpoint = webApp.ApplyEndpointFilter();
        return builderWithAppliedEndpoint.MapGroup(ApiRoute);
    }

    void IConfigureRoutesStartupFeature.ConfigureRoutes(IEndpointRouteBuilder webApp) => ConfigureRoutes(webApp);
}
