namespace ViixsDockerManager.DockLight.Models.Commands;

internal record RestartContainerCommand(Guid EnvironmentId, string ContainerId, string ContainerName)
        : BaseContainerActionCommand(EnvironmentId, ContainerId, ContainerName);
