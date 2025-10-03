using ViixsDockerManager.Mediator.Queries;
using ViixsDockerManager.Shared.Models.Dtos.DockLightEnvironments;

namespace ViixsDockerManager.Shared.Models.Queries.DockLightEnvironments;

/// <summary>
/// Represents a query for retrieving the set of supported Docker communication protocols for the initial environment.
/// </summary>
/// <remarks>Use this query to determine which Docker protocols are available for establishing connections in the
/// initial Docker environment. This can be useful for configuring clients or services that interact with Docker,
/// ensuring compatibility with the environment's supported protocols.</remarks>
public record GetPossibleDockerProtocolsQuery : IQuery<InitialDockerEnvironment>;
