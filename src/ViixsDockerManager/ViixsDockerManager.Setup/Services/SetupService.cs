using ViixsDockerManager.Setup.Exceptions;
using ViixsDockerManager.Setup.Models.Entities;
using ViixsDockerManager.Setup.Models.Entities.Filters;
using ViixsDockerManager.Shared.Database.Repositories;
using ViixsDockerManager.Shared.Helpers;

namespace ViixsDockerManager.Setup.Services;

internal class SetupService(
    IDatabaseReadRepository<SetupState> setupStateReadRepository,
    IDatabaseWriteRepository<SetupState> setupStateWriteRepository) : ISetupService
{
    public Task<SetupState?> GetFirstSetupState()
    {
        return setupStateReadRepository.GetFirstOrDefaultAsync();
    }

    public async Task<bool> IsSetupStarted()
    {
        var currentSetup = await GetFirstSetupState().ConfigureAwait(false);
        return currentSetup == null;
    }

    public async Task<bool> IsSetupFinished()
    {
        var currentSetup = await GetFirstSetupState().ConfigureAwait(false);
        return currentSetup != null && currentSetup.IsCompleted;
    }

    public async Task<SetupState> GetSetupStateBySetupId(Guid setupId)
    {
        setupId = Guard.ValueIsNotNull(setupId, nameof(setupId));
        var currentStep = await setupStateReadRepository.GetByFilterAsync(new GetStateBySetupIdFilter(setupId));

#pragma warning disable IDE0270 // Use coalesce expression
        if (currentStep is null)
        {
            throw new SetupIdDoesNotExistException(setupId);
        }
#pragma warning restore IDE0270 // Use coalesce expression
        return currentStep;
    }

    public Task SaveSetupState(SetupState setupState)
    {
        setupState = Guard.ValueIsNotNull(setupState, nameof(setupState));
        return setupStateWriteRepository.Save(setupState);
    }

    public Task UpdateSetupState(SetupState setupState)
    {
        setupState = Guard.ValueIsNotNull(setupState, nameof(setupState));
        return setupStateWriteRepository.Update(setupState);
    }
}
