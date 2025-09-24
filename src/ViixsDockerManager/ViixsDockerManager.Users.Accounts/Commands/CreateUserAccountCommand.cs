using ViixsDockerManager.Mediator.Commands;

namespace ViixsDockerManager.Users.Accounts.Commands;

internal record CreateUserAccountCommand(string EmailAddress, string Password, string Role = "")
    : ICommand;
