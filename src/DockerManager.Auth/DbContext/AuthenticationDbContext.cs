using DockerManager.Auth.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DockerManager.Auth.DbContext;

public class AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options)
    : IdentityDbContext<DockerManagerUser>(options)
{
}