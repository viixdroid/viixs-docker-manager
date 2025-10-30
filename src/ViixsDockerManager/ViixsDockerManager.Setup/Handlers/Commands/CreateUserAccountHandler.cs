using ViixsDockerManager.Mediator.Commands;
using ViixsDockerManager.Setup.Models.Commands;
using ViixsDockerManager.Setup.Services.Interfaces;
using ViixsDockerManager.Shared.Exceptions;
using ViixsDockerManager.Shared.Models.Errors;
using ViixsDockerManager.Shared.Models.Exceptions;
using ViixsDockerManager.Shared.Services;

namespace ViixsDockerManager.Setup.Handlers.Commands;

internal class CreateUserAccountHandler(
    ISharedUserAccountService sharedUserAccountService,
    ISendSetupStateNotifications sendSetupStateNotifications)
    : ICommandHandler<CreateFirstUserAccountCommand>
{
    public async Task Handle(CreateFirstUserAccountCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            await sharedUserAccountService.CreateUserAccount(command, cancellationToken);
        }
        catch (CouldNotCreateUserException e)
        {
            await sendSetupStateNotifications.SendOnUserCreationFailedAsync(e.Errors); 
            throw;
        }
        catch (ViixsDockerManagerException e)
        {
            await sendSetupStateNotifications.SendOnUserCreationFailedAsync([new ErrorDetail("VDMERR1", e.Message, "General")]);
            throw;
        }
    }
}
