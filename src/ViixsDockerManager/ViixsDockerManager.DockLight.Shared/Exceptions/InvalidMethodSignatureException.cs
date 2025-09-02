namespace ViixsDockerManager.DockLight.Shared.Exceptions;

public class InvalidMethodSignatureException(string methodName, int expectedParameters, int actualParameters)
    : DockerClientEndpointResolutionException($"Method '{methodName}' expected a signature with at least {expectedParameters} parameters, but found {actualParameters}.")
{
}
