namespace ViixsDockerManager.Shared.Models.Dtos.DockLightEnvironments;

/// <summary>
/// Represents the initial Docker environment configuration, including environment name, Docker runtime status, and
/// protocol information.
/// </summary>
/// <param name="Environment">The name of the OS in which the Docker container is running.</param>
/// <param name="IsRunningInDocker">true if the application is currently running inside a Docker container; otherwise, false.</param>
/// <param name="Protocol">The Docker protocol used by the environment.</param>
public record InitialDockerEnvironment(string Environment, bool IsRunningInDocker, DockerProtocol Protocol);
