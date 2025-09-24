using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ViixsDockerManager.Mediator.Commands;
using ViixsDockerManager.Users.Accounts.Commands;
using ViixsDockerManager.Users.Accounts.Models;
using ViixsDockerManager.Users.Accounts.Services.Interfaces;

using static ViixsDockerManager.Users.Accounts.Helpers.ViixsDockerManageRoleHelpers;

namespace ViixsDockerManager.Users.Accounts.Handlers;

internal class CreateUserAccountHandler(ISendUserAccountNotifications sendUserAccountNotifications,
    IUserStore<ViixsDockerManagerUser> userStore,
    UserManager<ViixsDockerManagerUser> userManager,
    ILogger<CreateUserAccountHandler> logger)
    : CommandHandlerBase<CreateUserAccountCommand, ISendUserAccountNotifications>(sendUserAccountNotifications, logger)
{
    protected override Task SendCommandNotificationAfterCommandAsync(ISendUserAccountNotifications commandNotificationSender, CreateUserAccountCommand command, bool executeCommandResult, CancellationToken cancellationToken = default)
        => commandNotificationSender.SendAccountCreatedNotificationAsync(command.EmailAddress, executeCommandResult, cancellationToken);

    protected override async Task<bool> ExecuteCommandAsync(CreateUserAccountCommand command, CancellationToken cancellationToken = default)
    {
        // Validate command
        var hasExisingUsers = await userManager.Users.AnyAsync(cancellationToken);

        var user = new ViixsDockerManagerUser();
        await userStore.SetUserNameAsync(user, command.EmailAddress, cancellationToken);
        await userManager.SetEmailAsync(user, command.EmailAddress);

        var createResult = await userManager.CreateAsync(user, command.Password);

        if (!createResult.Succeeded)
        {
            return false;
        }

        if (!hasExisingUsers)
        {
            await userManager.AddToRoleAsync(user, AdministratorRole);
        }

        if (string.IsNullOrEmpty(command.Role))
        {
            await userManager.AddToRoleAsync(user, command.Role);
        }
        return true;
    }

}
