using System.Net;
using ViixsDockerManager.Shared.Exceptions;

namespace ViixsDockerManager.Mediator.Exceptions;

public class CommandHandlerException(string message,  Exception? innerException = null)
    : ViixsDockerManagerWithHttpStatusCodeException(message, HttpStatusCode.InternalServerError, innerException)
{
}
