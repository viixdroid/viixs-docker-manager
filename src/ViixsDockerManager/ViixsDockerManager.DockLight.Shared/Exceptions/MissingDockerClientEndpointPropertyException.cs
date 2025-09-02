namespace ViixsDockerManager.DockLight.Shared.Exceptions;

public class MissingDockerClientEndpointPropertyException(string methodName, string targetEndpointTypeName, string interfaceName)
    : DockerClientEndpointResolutionException($"Method '{methodName}' second parameter type '{targetEndpointTypeName}' does not correspond to any public property in '{interfaceName}'.")
{
}
