using System.Net;
using Microsoft.Extensions.Logging;

namespace ViixsDockerManager.Shared.Exceptions;

public class ServiceFaultUnhandledException(string serviceName, string methodName, Exception? originalException)
    : ViixsDockerManagerWithHttpStatusCodeException(
        $"The service {serviceName}::{methodName} faulted.",
        HttpStatusCode.InternalServerError, originalException)
{
    protected override void LogException(ILogger logger, LogLevel level = LogLevel.Error)
    {
        logger.LogError(InnerException,
            "The service method {Service}::{Method} threw and exception with message: {ExceptionMessage}",
            serviceName, methodName, InnerException?.Message ?? "The original exception was null");
    }
}
