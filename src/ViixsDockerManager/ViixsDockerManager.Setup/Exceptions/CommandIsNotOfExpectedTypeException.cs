using ViixsDockerManager.Mediator.Commands;
using ViixsDockerManager.Setup.Handlers.Commands;

namespace ViixsDockerManager.Setup.Exceptions;

public class CommandIsNotOfExpectedTypeException(Type? requestedCommandType)
    : Exception($"Command {requestedCommandType?.Name} is not expected. Did you add this command to the registery in the {typeof(SetupHandler<>).Name}")
{
}
