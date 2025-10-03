namespace ViixsDockerManager.Setup.Models.Dtos;

/// <summary>
/// Represents the current step in a setup or configuration process, including its name and an optional unique
/// identifier.
/// </summary>
/// <param name="CurrentStep">The name or description of the current setup step. Cannot be null.</param>
/// <param name="StepId">The unique identifier associated with the current setup step, or null if the setup is just started</param>
internal record SetupStep(string CurrentStep, string NextStep, Guid? StepId);
