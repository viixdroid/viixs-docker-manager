using ViixsDockerManager.DockLight.Models.Overview;
using ViixsDockerManager.Mediator.Queries;

namespace ViixsDockerManager.DockLight.Models.Queries;

internal record GetContainersForEnvironmentQuery(Guid EnvironmentId)
    : IQuery<IEnumerable<ContainerSummary>>, IEnvironmentContext;
