using ViixsDockerManager.Setup.Models.Dtos;

namespace ViixsDockerManager.Setup.Controllers.Hubs;

public interface ISetupInformationContext
{
    Task NextSetupStepAsync(SetupStep setupStep);
    Task OnSetupStarted(SetupStep setupStep);
    Task OnUserCreated(SetupStep setupStep);
    Task OnEnvironmentCreated(SetupStep setupStep);
    Task OnSetupFinished(SetupStep setupStep);
}
