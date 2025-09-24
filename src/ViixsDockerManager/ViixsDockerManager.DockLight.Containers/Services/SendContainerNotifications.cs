using Microsoft.AspNetCore.SignalR;
using ViixsDockerManager.DockLight.Controllers.Hubs;
using ViixsDockerManager.DockLight.Models.Commands;
using ViixsDockerManager.DockLight.Services.Interfaces;

namespace ViixsDockerManager.DockLight.Services;

internal class SendContainerNotifications(IHubContext<DockLightInformationHub, IDockLightInformationContext> dockLightInformationContext) : ISendContainerNotifications
{
    public Task SendContainerStarted(BaseContainerActionCommand containerActionCommand, bool isSuccessfullyStarted)
    {
        var environmentId = containerActionCommand.EnvironmentId.ToString();
        return dockLightInformationContext.Clients.Group(environmentId).OnContainerStarted(containerActionCommand.ContainerId, containerActionCommand.ContainerName, isSuccessfullyStarted);
    }

    public Task SendContainerStopped(BaseContainerActionCommand containerActionCommand)
    {
        var environmentId = containerActionCommand.EnvironmentId.ToString();
        return dockLightInformationContext.Clients.Group(environmentId).OnContainerStopped(containerActionCommand.ContainerId, containerActionCommand.ContainerName);
    }

    public Task SendContainerRestarted(BaseContainerActionCommand containerActionCommand)
    {
        var environmentId = containerActionCommand.EnvironmentId.ToString();
        return dockLightInformationContext.Clients.Group(environmentId).OnContainerRestarted(containerActionCommand.ContainerId, containerActionCommand.ContainerName);
    }

    public Task SendContainerKilled(BaseContainerActionCommand containerActionCommand)
    {
        var environmentId = containerActionCommand.EnvironmentId.ToString();
        return dockLightInformationContext.Clients.Group(environmentId).OnContainerKilled(containerActionCommand.ContainerId, containerActionCommand.ContainerName);
    }
}
