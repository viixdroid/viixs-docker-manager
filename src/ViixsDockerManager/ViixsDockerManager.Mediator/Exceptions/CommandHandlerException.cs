using System.Net;
using ViixsDockerManager.Shared.Exceptions;

namespace ViixsDockerManager.Mediator.Exceptions;

public class CommandHandlerException(string message,  Exception? innerException = null)
    : ViixDockerManagerWithHttpStatusCodeException(message, HttpStatusCode.InternalServerError, innerException)
{
}
