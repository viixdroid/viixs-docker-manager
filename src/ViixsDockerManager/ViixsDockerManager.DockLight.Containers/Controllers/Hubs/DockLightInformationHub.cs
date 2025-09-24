using Microsoft.AspNetCore.SignalR;
using ViixsDockerManager.DockLight.Exceptions;

namespace ViixsDockerManager.DockLight.Controllers.Hubs;

internal class DockLightInformationHub : Hub<IDockLightInformationContext>
{
    private const string QueryStringEnvironmentIdKey = "environmentId";

    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var environmentIdString = httpContext?.Request.Query[QueryStringEnvironmentIdKey].ToString();

        if (!Guid.TryParse(environmentIdString, out var environmentId))
        {
            throw new ConnectionHubInvalidEnvironmentId(environmentIdString!);
        }

        var groupName = environmentId.ToString();
        await AddToGroup(groupName).ConfigureAwait(false);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var httpContext = Context.GetHttpContext();
        var environmentIdString = httpContext?.Request.Query[QueryStringEnvironmentIdKey].ToString();
        if (Guid.TryParse(environmentIdString, out var environmentId))
        {
            var groupName = environmentId.ToString();
            await RemoveFromGroup(groupName).ConfigureAwait(false);
        }
        await base.OnDisconnectedAsync(exception).ConfigureAwait(false);
    }


    private Task AddToGroup(string environmentId)
        => Groups.AddToGroupAsync(Context.ConnectionId, environmentId);

    private Task RemoveFromGroup(string environmentId)
        => Groups.RemoveFromGroupAsync(Context.ConnectionId, environmentId);
}

