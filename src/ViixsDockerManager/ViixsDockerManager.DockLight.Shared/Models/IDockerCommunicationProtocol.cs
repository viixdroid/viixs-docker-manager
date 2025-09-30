namespace ViixsDockerManager.DockLight.Shared.Models;

public interface IDockerCommunicationProtocol
{
    string Protocol { get; }

    /// <summary>
    /// Gets the protocol uri for docker communcation
    /// </summary>
    /// <returns>An uri which can connect to a docker instance</returns>
    Uri GetProtocolUri();
}
