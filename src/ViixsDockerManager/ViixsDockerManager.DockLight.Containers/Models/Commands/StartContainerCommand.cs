namespace ViixsDockerManager.DockLight.Models.Commands;

internal record StartContainerCommand(Guid EnvironmentId, string ContainerId, string ContainerName)
    : BaseContainerActionCommand(EnvironmentId, ContainerId, ContainerName);
