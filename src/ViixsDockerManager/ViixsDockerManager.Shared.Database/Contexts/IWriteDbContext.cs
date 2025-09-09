namespace ViixsDockerManager.Shared.Database.Contexts;

/// <summary>
/// Specifies that this dbcontext can only be used for saving, updating and deleting.
/// </summary>
/// <remarks>
/// While this specifies that the implemented context can be used for saving, updating and deleting
/// it is allowed to do read actions with this repository. That way we can ensure tracking if we
/// need to find a entity first.
/// </remarks>
public interface IWriteDbContext
{
}
