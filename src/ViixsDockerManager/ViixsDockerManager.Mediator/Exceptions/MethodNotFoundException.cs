namespace ViixsDockerManager.Mediator.Exceptions;

public class MethodNotFoundException(string methodName, Type methodOnType)
    : Exception($"Could not find {methodName} on {methodOnType.FullName}")
{

}
