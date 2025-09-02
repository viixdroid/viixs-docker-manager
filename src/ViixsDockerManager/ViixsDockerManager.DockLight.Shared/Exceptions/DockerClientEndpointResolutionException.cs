namespace ViixsDockerManager.DockLight.Shared.Exceptions;

public class DockerClientEndpointResolutionException(string message, Exception? innerException = null)
    : Exception(message, innerException)
{
}
