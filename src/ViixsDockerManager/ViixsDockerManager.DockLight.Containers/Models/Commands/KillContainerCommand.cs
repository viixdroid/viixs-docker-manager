namespace ViixsDockerManager.DockLight.Models.Commands;

internal record KillContainerCommand(Guid EnvironmentId, string ContainerId, string ContainerName)
    : BaseContainerActionCommand(EnvironmentId, ContainerId, ContainerName);
