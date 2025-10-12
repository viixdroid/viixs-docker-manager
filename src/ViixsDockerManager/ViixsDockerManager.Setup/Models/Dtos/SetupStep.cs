using ViixsDockerManager.Setup.Models.Entities;
using ViixsDockerManager.Shared.Helpers;

namespace ViixsDockerManager.Setup.Models.Dtos;

public sealed record SetupStep(Guid SetupId, string CurrentStep)
{
    public static implicit operator SetupStep(SetupState setupState)
    {
        setupState = Guard.ValueIsNotNull(setupState, nameof(setupState));
        return new SetupStep(setupState.SetupId, setupState.CurrentStep);
    }
}
