namespace ViixsDockerManager.DockLight.Shared.Exceptions;

public class ContainerNotFoundException(string containerId)
    : Exception($"Container with Id {containerId} could not be found")
{
}
