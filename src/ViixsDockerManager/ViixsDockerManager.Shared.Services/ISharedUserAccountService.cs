using ViixsDockerManager.Shared.Models.Commands.Users;

namespace ViixsDockerManager.Shared.Services;

public interface ISharedUserAccountService
{
    Task CreateUserAccount(CreateUserAccountCommand createUserAccountCommand, CancellationToken cancellationToken = default);
}
