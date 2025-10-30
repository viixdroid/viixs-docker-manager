using ViixsDockerManager.Setup.Models.Dtos;
using ViixsDockerManager.Shared.Models.Errors;

namespace ViixsDockerManager.Setup.Controllers.Hubs;

public interface ISetupInformationContext
{
    Task OnNextSetupStep(SetupStep setupStep);
    Task OnSetupStarted(SetupStep setupStep);
    Task OnUserCreationFailed(List<ErrorDetail> errorDetails);
    Task OnEnvironmentCreated(SetupStep setupStep);
    Task OnSetupFinished(SetupStep setupStep);
}
