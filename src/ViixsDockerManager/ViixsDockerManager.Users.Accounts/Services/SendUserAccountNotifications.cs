using Microsoft.AspNetCore.SignalR;
using ViixsDockerManager.Users.Accounts.Controllers.Hubs;
using ViixsDockerManager.Users.Accounts.Services.Interfaces;

namespace ViixsDockerManager.Users.Accounts.Services;

internal class SendUserAccountNotifications(IHubContext<UserAccountHub, IUserAccountContext> userAccountContext) : ISendUserAccountNotifications
{
    public Task SendAccountCreatedNotificationAsync(string emailAddress, bool accountCreatedSuccesfully, CancellationToken cancellationToken = default)
        => userAccountContext.Clients.All.OnAccountCreated(emailAddress, accountCreatedSuccesfully);
}
