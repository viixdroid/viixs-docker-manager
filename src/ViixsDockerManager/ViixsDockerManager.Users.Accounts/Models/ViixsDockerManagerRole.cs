using Microsoft.AspNetCore.Identity;

namespace ViixsDockerManager.Users.Accounts.Models;

internal class ViixsDockerManagerRole : IdentityRole
{
    public ViixsDockerManagerRole()
    {
        
    }

    public ViixsDockerManagerRole(string roleName)
        : base(roleName)
    {
        
    }
}
