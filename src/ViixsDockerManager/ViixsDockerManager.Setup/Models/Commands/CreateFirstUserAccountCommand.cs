using ViixsDockerManager.Mediator.Commands;
using ViixsDockerManager.Shared.Models.Commands.Users;
using static ViixsDockerManager.Shared.Constants.Users;

namespace ViixsDockerManager.Setup.Models.Commands;

internal record CreateFirstUserAccountCommand(string EmailAddress, string Password, string Role = Roles.Administrator)
    : CommandBase, ICreateUserAccountCommand;
