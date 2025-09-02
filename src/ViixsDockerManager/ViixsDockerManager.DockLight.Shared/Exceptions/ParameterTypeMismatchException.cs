namespace ViixsDockerManager.DockLight.Shared.Exceptions;

public class ParameterTypeMismatchException(string parameterName, Type expectedType, Type actualType)
    : DockerClientEndpointResolutionException($"Parameter '{parameterName}' expected type {expectedType.Name} but was {actualType.Name}.")
{
}
