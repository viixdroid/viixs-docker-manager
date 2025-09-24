using ViixsDockerManager.Mediator;
using ViixsDockerManager.Shared.Services;

namespace ViixsDockerManager.Users.Accounts.Services.Interfaces;

public interface ISendUserAccountNotifications : ICommandNotificationSender, IViixsBaseService
{
    Task SendAccountCreatedNotificationAsync(string emailAddress, bool accountCreatedSuccesfully, CancellationToken cancellationToken = default);
}
