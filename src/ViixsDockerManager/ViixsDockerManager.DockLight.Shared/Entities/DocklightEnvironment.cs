using ViixsDockerManager.Shared.Database.Entities;

namespace ViixsDockerManager.DockLight.Shared.Entities;

public class DocklightEnvironment : BaseEntity
{
    /// <summary>
    /// The databaseid of this database object
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// A unique Id for getting the environment
    /// </summary>
    public Guid EnvironmentId { get; set; }
    /// <summary>
    /// The name of this database object
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// The location where the Environment can be found.
    ///
    /// For local connections this will /var/docker/docker.sock
    ///
    /// for external connections, this can be an ip address.
    /// </summary>
    public required string ApiLocation { get; set; }

}
