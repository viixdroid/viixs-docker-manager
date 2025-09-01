using static ViixsDockerManager.DockLight.Constants.DockLightConstants;

namespace ViixsDockerManager.DockLight.Models;

internal record DockerCommunicationProtocol
{
    private readonly string _protocol;
    private readonly string _address;

    private DockerCommunicationProtocol(string protocol, string address)
    {
        _protocol = protocol;
        _address = address;
    }

    /// <summary>
    /// Gets the protocol uri for docker communcation
    /// </summary>
    /// <returns>An uri which can connect to a docker instance</returns>
    public Uri GetProtocolUri()
    {
        return new Uri($"{_protocol}{GetAddress()}");
    }

    private string GetAddress()
    {
        return _protocol switch
        {
            Unix.Protocol => $"://{_address}",
            _ => throw new NotSupportedException($"The protocol '{_protocol}' is not supported.")
        };
    }

    /// <summary>
    /// Checks if a specific address exists as a file. When in a docker container
    /// the docker.sock (unix) and docker_engine npipe (windows) are mounted as
    /// files. So we use this method to check if they exists.
    /// </summary>
    /// <returns>
    /// true if there is a file with the _address
    /// otherwise false.
    /// </returns>
    public bool AddressExists()
    {
        return _protocol switch
        {
            Unix.Protocol =>
                // In a docker container, the unix socket is mounted as a file.
                // So we check if the file exists.
                File.Exists(_address),
            _ => false
        };
    }

    public static DockerCommunicationProtocol UnixCommunication() =>
        new DockerCommunicationProtocol(Unix.Protocol, Unix.Socket);
}
