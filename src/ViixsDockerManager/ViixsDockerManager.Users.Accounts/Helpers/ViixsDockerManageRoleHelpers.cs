using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ViixsDockerManager.Users.Accounts.Models;

namespace ViixsDockerManager.Users.Accounts.Helpers;

internal static class ViixsDockerManageRoleHelpers
{
    public const string AdministratorRole = "Administrator";
    public static async Task SeedApplicationRoles(this IServiceProvider serviceProvider)
    {
        var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(ViixsDockerManageRoleHelpers));
        var roleManager = serviceProvider.GetRequiredService<RoleManager<ViixsDockerManagerRole>>();
        List<string> roles = [AdministratorRole];

        foreach (var role in roles)
        {
            var roleExists = await roleManager.RoleExistsAsync(role);
            if (!roleExists)
            {
                var isCreated = await roleManager.CreateAsync(new ViixsDockerManagerRole(role));
                if (!isCreated.Succeeded)
                {
                    logger.LogWarning("Could not create the inital role for {Role}", role);
                }
            }
        }
    }
}
