using Microsoft.AspNetCore.Identity;
using ViixsDockerManager.Shared.Helpers;
using ViixsDockerManager.Shared.Models.Commands.Users;
using ViixsDockerManager.Shared.Services;
using ViixsDockerManager.Users.Accounts.Exceptions;
using ViixsDockerManager.Users.Accounts.Models;

namespace ViixsDockerManager.Users.Accounts.Services;

internal class UserAccountService(IUserStore<ViixsDockerManagerUser> userStore,
    UserManager<ViixsDockerManagerUser> userManager
    )
    : IUserAccountService, ISharedUserAccountService
{
    public async Task CreateUserAccount(CreateUserAccountCommand createUserAccountCommand, CancellationToken cancellationToken = default)
    {
        // Validate command

        var user = new ViixsDockerManagerUser();
        await userStore.SetUserNameAsync(user, createUserAccountCommand.EmailAddress, cancellationToken);
        await userManager.SetEmailAsync(user, createUserAccountCommand.EmailAddress);

        var createResult = await userManager.CreateAsync(user, createUserAccountCommand.Password);

        if (!createResult.Succeeded)
        {
            throw new CouldNotCreateUserException(createResult.ToString());
        }

        var role = Guard.ValueIsNotNullOrEmpty(createUserAccountCommand.Role, nameof(createUserAccountCommand.Role));

        await userManager.AddToRoleAsync(user, role);
    }
}
