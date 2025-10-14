using ViixsDockerManager.Shared.Models.Commands.Users;

namespace ViixsDockerManager.Users.Accounts.Services;

internal interface IUserAccountService
{
    Task CreateUserAccount(CreateUserAccountCommand createUserAccountCommand, CancellationToken cancellationToken = default);
}
