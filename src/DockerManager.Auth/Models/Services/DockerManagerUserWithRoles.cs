namespace DockerManager.Auth.Models.Services;

public record DockerManagerUserWithRoles(DockerManagerUser DockerManagerUser, IEnumerable<string> Roles);