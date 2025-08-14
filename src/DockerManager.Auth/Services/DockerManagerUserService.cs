using DockerManager.Auth.Models;
using DockerManager.Auth.Models.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DockerManager.Auth.Services;

public class DockerManagerUserService(UserManager<DockerManagerUser> userManager) : IDockerManagerUserService
{
    public Task<bool> UserExists(string email)
    {
        return userManager.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<DockerManagerUserWithRoles?> GetUserByEmailWithRolesTask(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return null;
        }

        var roles = await userManager.GetRolesAsync(user);
        return new DockerManagerUserWithRoles(user, roles);
    }
}