using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ViixsDockerManager.Mediator;
using ViixsDockerManager.Users.Accounts.Commands;

namespace ViixsDockerManager.Users.Accounts.Controllers;

internal static class UsersAccountRouteActions
{
    // /user/register
    // /user/login
    // /user/refresh
    // /user/logout
    // /user/change-password
    // /user/reset-password
    // /user/confirm-email
    // /user/send-confirmation-email
    // /user/me
    // /user/update-profile
    // /user/delete-account
    // /user/external-login
    // /user/external-callback
    // /user/link-external
    // /user/unlink-external

    public static RouteGroupBuilder MapUserAccountRouteActions(this RouteGroupBuilder builder)
    {
        builder.MapPost("/register", CreateUserAccount);
        builder.MapPost("/createuser", CreateUserAccount);
        //builder.MapPost("/login", () => Results.Ok("Login endpoint"));
        //builder.MapPost("/refresh", () => Results.Ok("Refresh endpoint"));
        //builder.MapPost("/logout", () => Results.Ok("Logout endpoint"));
        //builder.MapGet("{userId:guid}/me", (Guid userId) => Results.Ok("Me endpoint"));
        return builder;
    }

    public static Task CreateUserAccount([FromBody] CreateUserAccountCommand createUserAccountCommand, IMediator mediator)
    {
        return mediator.Send(createUserAccountCommand);
    }
}
