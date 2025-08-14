using DockerManager.Auth.Models.Services;

namespace DockerManager.Auth.Services;

public interface IDockerManagerUserService
{
    Task<bool> UserExists(string email);
    Task<DockerManagerUserWithRoles?> GetUserByEmailWithRolesTask(string email);
}