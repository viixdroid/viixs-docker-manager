using ViixsDockerManager.Shared.Database.Entities;
using ViixsDockerManager.Shared.Helpers;
using ViixsDockerManager.Shared.Models.Commands.DockLightEnvironments;

namespace ViixsDockerManager.DockLight.Shared.Entities;

public class DockLightEnvironment : BaseEntity
{
    /// <summary>
    /// A unique Id for getting the environment
    /// </summary>
    public Guid EnvironmentId { get; set; }
    /// <summary>
    /// The name of this database object
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// The location where the Environment can be found.<br />
    ///
    /// For local docker (Linux image) and or Linux connections this will /var/docker/docker.sock <br />
    ///
    /// For local Windows connections, this will be /pipe/docker_engine <br />
    ///
    /// for external connections, this can be an ip address.<br />
    /// </summary>
    public required string ApiLocation { get; set; }
    
}
