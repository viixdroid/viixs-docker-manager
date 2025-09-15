using ViixsDockerManager.Shared.Services;

namespace ViixsDockerManager.DockLight.Services.Interfaces;

internal interface ISendContainerNotifications : IViixsBaseService
{
    Task SendContainerStarted(Guid environmentId, string containerName, bool isSuccessfullyStarted);
}
