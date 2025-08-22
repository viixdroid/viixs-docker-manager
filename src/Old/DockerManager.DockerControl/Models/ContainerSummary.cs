using Docker.DotNet.Models;

namespace DockerManager.DockerControl.Models;

public record ContainerSummary(
    string Id,
    string Name,
    string Image,
    IList<ContainerPort> Ports,
    DateTime Created,
    ContainerState State)
{
    private const char StartChar = '/';
    public string CleanedName => Name.StartsWith(StartChar) ? Name[1..] : Name;

    public static implicit operator ContainerSummary(ContainerListResponse response)
    {
        var containerState = Enum.TryParse<ContainerState>(response.State, ignoreCase: true, out var state)
            ? state
            : ContainerState.Unknown; // Default to Exited if parsing fails

        return new ContainerSummary(
            response.ID,
            response.Names.Count > 0 ? response.Names[0] : "N/A",
            response.Image,
            response.Ports.Select(p => (ContainerPort)p).ToList(),
            response.Created,
            containerState);
    }
}
