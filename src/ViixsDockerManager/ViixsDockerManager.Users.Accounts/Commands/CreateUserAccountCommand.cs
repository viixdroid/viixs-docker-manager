using ViixsDockerManager.Mediator.Commands;
using ViixsDockerManager.Shared.Models.Commands.Users;
using static ViixsDockerManager.Shared.Constants.Users;

namespace ViixsDockerManager.Users.Accounts.Commands;

/// <summary>
/// Represents a command to create a new user account with the specified email address, password, and role.
/// </summary>
/// <param name="EmailAddress">The email address to associate with the new user account. Cannot be null or empty.</param>
/// <param name="Password">The password to set for the new user account. Cannot be null or empty.</param>
/// <param name="Role">The role to assign to the new user account. If not specified, the default role is used.</param>
internal record CreateUserAccountCommand(string EmailAddress, string Password, string Role = Roles.None)
    : CommandBase, ICreateUserAccountCommand;
