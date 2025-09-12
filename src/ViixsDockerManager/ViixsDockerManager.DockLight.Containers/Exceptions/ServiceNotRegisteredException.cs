namespace ViixsDockerManager.DockLight.Exceptions;

public class ServiceNotRegisteredException(Type serviceType)
    : Exception($"The service {serviceType.FullName+26} is not registered. Please add it to the service factory cache.")
{
}
