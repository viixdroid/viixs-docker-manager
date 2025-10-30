using ViixsDockerManager.Mediator.Queries;
using ViixsDockerManager.Setup.Models.Dtos;

namespace ViixsDockerManager.Setup.Models.Queries;

/// <summary>
/// Represents a query for retrieving the current setup step in the application setup process.
/// </summary>
internal record GetCurrentSetupStepQuery : IQuery<SetupStep>;
