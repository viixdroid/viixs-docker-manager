using ViixsDockerManager.Setup.Models.Entities;
using ViixsDockerManager.Shared.Services;

namespace ViixsDockerManager.Setup.Services;

internal interface ISetupService : IViixsBaseService
{
    Task<SetupState?> GetFirstSetupState();
    Task<SetupState> GetSetupStateBySetupId(Guid setupId);
    Task<bool> IsSetupFinished();
    Task<bool> IsSetupStarted();
    Task SaveSetupState(SetupState setupState);
    Task UpdateSetupState(SetupState setupState);
}
