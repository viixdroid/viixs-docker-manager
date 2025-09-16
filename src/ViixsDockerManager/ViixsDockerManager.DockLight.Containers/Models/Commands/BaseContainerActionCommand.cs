using ViixsDockerManager.Mediator.Commands;

namespace ViixsDockerManager.DockLight.Models.Commands;

internal abstract record BaseContainerActionCommand(Guid EnvironmentId, string ContainerId, string ContainerName)
    : ICommand, IEnvironmentContext;
