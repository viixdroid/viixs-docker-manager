using System.Text.RegularExpressions;
using Microsoft.AspNetCore.SignalR;
using ViixsDockerManager.DockLight.Controllers.Hubs;
using ViixsDockerManager.DockLight.Services.Interfaces;

namespace ViixsDockerManager.DockLight.Services;

internal class SendContainerNotifications(IHubContext<DockLightInformationHub, IDockLightInformationContext> dockLightInformationContext) : ISendContainerNotifications
{
    public Task SendContainerStarted(Guid environmentId, string containerId, string containerName, bool isSuccessfullyStarted)
    {
        return dockLightInformationContext.Clients.Group(environmentId.ToString()).OnContainerStarted(containerId, containerName, isSuccessfullyStarted);
    }
}
