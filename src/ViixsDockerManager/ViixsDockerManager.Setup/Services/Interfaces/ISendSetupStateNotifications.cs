using ViixsDockerManager.Setup.Models.Dtos;
using ViixsDockerManager.Shared.Services;

namespace ViixsDockerManager.Setup.Services.Interfaces;

internal interface ISendSetupStateNotifications : IViixsBaseService
{
    Task SendOnEnvironmentCreated(SetupStep setupStep, object validationInformation);
    Task SendOnSetupFinishedAsync(SetupStep setupStep);
    Task SendOnSetupStartedAsync(string connectionId, SetupStep setupStep);
    Task SendOnUserCreatedAsync(SetupStep setupStep, object validationInformation);
}
