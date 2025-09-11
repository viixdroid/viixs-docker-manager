using Docker.DotNet.Models;
using ViixsDockerManager.Shared.Helpers;

namespace ViixsDockerManager.DockLight.Models.Details;

internal record ContainerStatus(
    ContainerHealth Health,
    ContainerState state,
    DateTime? StartedTime
    )
{
    public static implicit operator ContainerStatus(State state)
    {
        state = Guard.ValueIsNotNull(state, nameof(state));

        //TODO: Check if we want to parse health it's own object?
        var containerHealth = ContainerHealth.Unknown;
        if (state.Health != null && Enum.TryParse<ContainerHealth>(state.Health.Status, out var parsedContainerHealth))
        {
            containerHealth = parsedContainerHealth; 
        }

        var containerState = ContainerState.Unknown;
        if (Enum.TryParse<ContainerState>(state.Status, ignoreCase: true, out var parsedContainerState))
        {
            containerState = parsedContainerState;
        }

        DateTime? startedTime = null;
        if (DateTime.TryParse(state.StartedAt, out DateTime parsedStarteAt))
        {
            startedTime = parsedStarteAt;
        }

        return new ContainerStatus(containerHealth, containerState, startedTime);
    }
}
