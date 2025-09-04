namespace ViixsDockerManager.Shared.Database.Entities;

public abstract class BaseEntity : IEntity
{
    /// <summary>
    /// The databaseid of this database object
    /// </summary>
    public int Id { get; set; }
}
