namespace ViixsDockerManager.DockLight.Shared.Exceptions;

public class InvalidArgumentTypeException(string argumentDescription, Type expectedType, Type? actualType)
    : DockerClientEndpointResolutionException($"Argument '{argumentDescription}' expected type {expectedType.Name} but was {actualType?.Name ?? "null"}.")
{

}
