using ViixsDockerManager.Mediator.Commands;
using ViixsDockerManager.Setup.Exceptions;
using ViixsDockerManager.Setup.Models;
using ViixsDockerManager.Setup.Models.Commands;
using ViixsDockerManager.Setup.Models.Entities;
using ViixsDockerManager.Setup.Models.Entities.Filters;
using ViixsDockerManager.Setup.Services.Interfaces;
using ViixsDockerManager.Shared.Database.Repositories;
using ViixsDockerManager.Shared.Helpers;
using ViixsDockerManager.Shared.Models.Commands.Users;
using ViixsDockerManager.Shared.Services;

namespace ViixsDockerManager.Setup.Handlers.Commands;

internal class CreateUserAccountHandler(
    ISharedUserAccountService sharedUserAccountService,
    ISendSetupStateNotifications sendSetupStateNotifications)
    : ICommandHandler<SetupCommand<CreateUserAccountCommand>>
{
    public async Task Handle(SetupCommand<CreateUserAccountCommand> command, CancellationToken cancellationToken = default)
    {



        await sharedUserAccountService.CreateUserAccount(command.InternalCommand, cancellationToken);

    }
}
