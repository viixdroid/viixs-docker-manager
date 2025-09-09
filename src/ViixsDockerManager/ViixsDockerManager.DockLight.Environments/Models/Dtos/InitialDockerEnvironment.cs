namespace ViixsDockerManager.DockLight.Environments.Models.Dtos;

public record InitialDockerEnvironment(string Environment, bool IsRunningInDocker, DockerProtocol Protocol);
