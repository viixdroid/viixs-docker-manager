using ViixsDockerManager.DockLight.Models.Details;
using ViixsDockerManager.Mediator.Queries;

namespace ViixsDockerManager.DockLight.Models.Queries;

internal record GetContainerDetailsQuery(Guid EnvironmentId, string ContainerId)
    : DockerBaseQuery(EnvironmentId), IQuery<ContainerDetails>;
