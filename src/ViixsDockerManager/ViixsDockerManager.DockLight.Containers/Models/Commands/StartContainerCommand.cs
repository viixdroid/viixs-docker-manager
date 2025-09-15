
using ViixsDockerManager.Mediator.Commands;

namespace ViixsDockerManager.DockLight.Models.Commands;

internal record StartContainerCommand(Guid EnvironmentId, string ContainerId, string ContainerName)
    : ICommand, IEnvironmentContext;
