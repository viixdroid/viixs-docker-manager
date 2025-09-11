using ViixsDockerManager.DockLight.Models.Overview;
using ViixsDockerManager.Mediator.Queries;

namespace ViixsDockerManager.DockLight.Models.Queries;

internal record GetContainersForEnvironmentQuery(Guid EnvironmentId)
    : DockerBaseQuery(EnvironmentId), IQuery<IEnumerable<ContainerSummary>>;
