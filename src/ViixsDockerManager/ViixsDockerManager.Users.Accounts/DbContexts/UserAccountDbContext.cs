using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ViixsDockerManager.Users.Accounts.Models;
using ViixsDockerManager.Shared.Database.Contexts;
using Microsoft.AspNetCore.Identity;

namespace ViixsDockerManager.Users.Accounts.DbContexts;

internal class UserAccountDbContext(DbContextOptions<UserAccountDbContext> options)
    : IdentityDbContext<ViixsDockerManagerUser, ViixsDockerManagerRole, string>(options), IReadDbContext, IWriteDbContext
{
}
