using ViixsDockerManager.Server.Startup.Factories;
using ViixsDockerManager.Shared.AspNet.Extensions;
using ViixsDockerManager.Shared.AspNet.Startup.Interfaces;
using ViixsDockerManager.Shared.Helpers;

namespace ViixsDockerManager.Server.Startup;

internal sealed class RouteStartupFeature(IBuilderCollectionFactory builderCollectionFactory) : StartupFeature
{
    private const string WebSocketRoute = "ws";
    private const string ApiRoute = "api";


    protected override void ConfigureBuilder(WebApplicationBuilder webAppbuilder) { }
    protected override void ConfigureApplication(WebApplication webApp)
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

    private static IEndpointRouteBuilder CreateDefaultWebSocketRoute(WebApplication webApp)
        => webApp.MapGroup(WebSocketRoute);

    private static IEndpointRouteBuilder CreateDefaultApiRoute(WebApplication webApp)
    {
        var builderWithAppliedEndpoint = webApp.ApplyEndpointFilter();
        return builderWithAppliedEndpoint.MapGroup(ApiRoute);
    }
}
