using System.Text.Json.Serialization;
using Docker.DotNet.Models;

namespace ViixsDockerManager.DockLight.Models.Overview;

public record ContainerSummary(
    string Id,
    string Name,
    string Image,
    IList<ContainerPort> Ports,
    DateTime Created,
    ContainerState State)
{
    private const char StartChar = '/';
    [JsonIgnore]
    public string CleanedName => Name.StartsWith(StartChar) ? Name[1..] : Name;

    private static string GetCleanedName(string containerName) =>
        containerName.StartsWith(StartChar) ? containerName[1..] : containerName;

    public static implicit operator ContainerSummary(ContainerListResponse response)
    {
        var containerState = Enum.TryParse<ContainerState>(response.State, ignoreCase: true, out var state)
            ? state
            : ContainerState.Unknown; // Default to Exited if parsing fails

        return new ContainerSummary(
            response.ID,
            response.Names.Count > 0 ? GetCleanedName(response.Names[0]) : "N/A",
            response.Image,
            [.. response.Ports.Select(p => (ContainerPort)p)],
            response.Created,
            containerState);
    }
}
