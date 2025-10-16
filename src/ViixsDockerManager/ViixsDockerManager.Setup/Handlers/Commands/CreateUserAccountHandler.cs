using ViixsDockerManager.Mediator.Commands;
using ViixsDockerManager.Setup.Models.Commands;
using ViixsDockerManager.Setup.Services.Interfaces;
using ViixsDockerManager.Shared.Services;

namespace ViixsDockerManager.Setup.Handlers.Commands;

internal class CreateUserAccountHandler(
    ISharedUserAccountService sharedUserAccountService,
    ISendSetupStateNotifications sendSetupStateNotifications)
    : ICommandHandler<CreateFirstUserAccountCommand>
{
    public async Task Handle(CreateFirstUserAccountCommand command, CancellationToken cancellationToken = default)
    {
        await sharedUserAccountService.CreateUserAccount(command, cancellationToken);
        //await sendSetupStateNotifications.SendOnUserCreatedAsync(null, null);
    }
}
