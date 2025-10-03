using ViixsDockerManager.Mediator.Commands;
using ViixsDockerManager.Setup.Models.Dtos;

namespace ViixsDockerManager.Setup.Models.Commands;

/// <summary>
/// Represents a command to set the current setup step in the application workflow.
/// </summary>
/// <param name="CurrentSetupStep">The setup step to set as the current step.</param>
internal record SetCurrentSetupStepCommand(SetupStep CurrentSetupStep)
    : ICommand;
