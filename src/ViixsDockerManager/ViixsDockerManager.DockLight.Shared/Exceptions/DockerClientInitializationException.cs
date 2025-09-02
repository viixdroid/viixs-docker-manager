namespace ViixsDockerManager.DockLight.Shared.Exceptions;

public class DockerClientInitializationException(string message, Exception? innerException = null)
    : DockerClientEndpointResolutionException(message, innerException)
{

}
