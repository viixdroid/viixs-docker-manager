using Docker.DotNet.Models;

namespace DockerManager.DockerControl.Models;

//TODO: State can be an enum
public record ContainerSummary(string Id, string Name, string Image, DateTime Created, string State)
{
    private const char StartChar = '/';
    public string CleanedName => Name.StartsWith(StartChar) ? Name[1..] : Name;
    public static implicit operator ContainerSummary(ContainerListResponse response)
    {
        return new ContainerSummary(
            response.ID,
            response.Names.Count > 0 ? response.Names[0] : "N/A",
            response.Image,
            response.Created,
            response.State);
    }
}
