using Microsoft.AspNetCore.SignalR;
using ViixsDockerManager.Shared.WebSockets.Interfaces;

namespace ViixsDockerManager.Setup.Controllers.Hubs;

internal class SetupInformationHub() : Hub<ISetupInformationContext>, IWebSocketHub
{
    //public override async Task OnConnectedAsync()
    //{
    //    var httpContext = Context.GetHttpContext();
    //    var setupIdString = httpContext?.Request.Query["setupId"].ToString();
    //    if (!Guid.TryParse(setupIdString, out var setupId))
    //    {
    //        return;
    //    }

    //    var groupName = setupId.ToString();
    //    await AddToGroup(groupName).ConfigureAwait(false);
    //    await base.OnConnectedAsync();
    //}

    //public override async Task OnDisconnectedAsync(Exception? exception)
    //{
    //    var context = Context.GetHttpContext();
    //    var setupIdString = context?.Request.Query["setupId"].ToString();
    //    if (Guid.TryParse(setupIdString, out var setupId))
    //    {
    //        var groupName = setupId.ToString();
    //        await RemoveFromGroup(groupName).ConfigureAwait(false);
    //    }

    //    await base.OnDisconnectedAsync(exception);
    //}

    //private Task AddToGroup(string setupId)
    //    => Groups.AddToGroupAsync(Context.ConnectionId, setupId);

    //private Task RemoveFromGroup(string setupId)
    //    => Groups.RemoveFromGroupAsync(Context.ConnectionId, setupId);
}
