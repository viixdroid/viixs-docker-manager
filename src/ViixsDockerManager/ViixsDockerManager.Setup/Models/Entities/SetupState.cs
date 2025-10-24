using ViixsDockerManager.Shared.Database.Entities;

namespace ViixsDockerManager.Setup.Models.Entities;

/// <summary>
/// Tracks the state of the setup in the database.
/// </summary>
public class SetupState() : BaseEntity
{
    public Guid SetupId { get; set; }
    public string? LastCompletedStep { get; set; }
    public required string CurrentStep { get; set; }
    public required DateTime LastUpdated { get; set; }
    public bool IsCompleted { get; set; }

    public void FinishSetup()
    {
        IsCompleted = true;
        CurrentStep = SetupStepName.Finish;
        LastUpdated = DateTime.UtcNow;
    }

    public void UpdateSetupState(SetupStepName lastCompletedStep, SetupStepName currentStep)
    {
        LastCompletedStep = lastCompletedStep;
        CurrentStep = currentStep;
        LastUpdated = DateTime.UtcNow;
    }
}
