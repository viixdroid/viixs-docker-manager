using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using ViixsDockerManager.Shared.AspNet.Startup;
using ViixsDockerManager.Users.Accounts.Controllers;
using ViixsDockerManager.Users.Accounts.Controllers.Hubs;

namespace ViixsDockerManager.Users.Accounts.Startup;

public sealed class UserAccountsRouteComponent : RouteComponent
{
    protected override void ConfigureWebApiRoutes(IEndpointRouteBuilder routeGroupBuilder)
    {
        var group = routeGroupBuilder.MapGroup("/users");
        group.MapUserAccountRouteActions();
    }

    protected override void ConfigureWebSocketRoutes(IEndpointRouteBuilder routeGroupBuilder)
    {
        var group = routeGroupBuilder.MapGroup("/users");
        group.MapHub<UserAccountHub>("/account");
    }
}
