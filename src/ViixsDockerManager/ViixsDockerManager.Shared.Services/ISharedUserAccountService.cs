using ViixsDockerManager.Shared.Models.Commands.Users;

namespace ViixsDockerManager.Shared.Services;

public interface ISharedUserAccountService
{
    Task CreateUserAccount(ICreateUserAccountCommand createUserAccountCommand, CancellationToken cancellationToken = default);
}
