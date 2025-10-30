using ViixsDockerManager.Shared.Models.Commands.Users;

namespace ViixsDockerManager.Users.Accounts.Services;

internal interface IUserAccountService
{
    Task CreateUserAccount(ICreateUserAccountCommand createUserAccountCommand, CancellationToken cancellationToken = default);
}
