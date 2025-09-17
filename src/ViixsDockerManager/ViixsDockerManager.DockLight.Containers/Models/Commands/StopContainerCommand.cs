namespace ViixsDockerManager.DockLight.Models.Commands;

internal record StopContainerCommand(Guid EnvironmentId, string ContainerId, string ContainerName)
    : BaseContainerActionCommand(EnvironmentId, ContainerId, ContainerName);
