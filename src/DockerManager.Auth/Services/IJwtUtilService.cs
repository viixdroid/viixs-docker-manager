using DockerManager.Auth.Models.Services;

namespace DockerManager.Auth.Services;

public interface IJwtUtilService
{
    string GenerateJwtToken(DockerManagerUserWithRoles dockerManagerUserWithRoles);
}