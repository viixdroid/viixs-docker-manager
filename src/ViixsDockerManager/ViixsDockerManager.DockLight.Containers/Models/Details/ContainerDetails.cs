using Docker.DotNet.Models;

namespace ViixsDockerManager.DockLight.Models.Details;

internal record ContainerDetails(
    string Id,
    string Name,
    DateTime CreatedTime,
    ContainerStatus Status
    )
{

    public static implicit operator ContainerDetails(ContainerInspectResponse inspectResponse)
    {
        return new ContainerDetails(
            inspectResponse.ID,
            inspectResponse.Name.StartsWith('/') ? inspectResponse.Name[1..] : inspectResponse.Name,
            inspectResponse.Created,
            (ContainerStatus)inspectResponse.State
            );
    }

}
