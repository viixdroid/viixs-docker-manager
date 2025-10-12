using ViixsDockerManager.Shared.Database.Entities;

namespace ViixsDockerManager.Setup.Models.Entities;

/// <summary>
/// Tracks the state of the setup in the database.
/// </summary>
public class SetupState() : BaseEntity
{
    public Guid SetupId { get; set; }
    public required string CurrentStep { get; set; }
    public required DateTime LastUpdated { get; set; }
    public bool IsCompleted { get; set; }
}
