using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using ViixsDockerManager.Shared.Exceptions;
using ViixsDockerManager.Shared.Models;

namespace ViixsDockerManager.Shared.Middlewares;

public class ViixsDockerManagerExceptionHandler(ILogger<ViixsDockerManagerExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        var message = "An unhandled exception occurred on the server";
        if (exception is ViixDockerManagerWithHttpStatusCodeException viixDockerManagerWithHttpStatusCodeException)
        {
            statusCode = viixDockerManagerWithHttpStatusCodeException.StatusCode;
            message = viixDockerManagerWithHttpStatusCodeException.Message;
        }

        httpContext.Response.StatusCode = (int)statusCode;
        var responseObject = ResponseObject.Failure(message);
        await httpContext.Response.WriteAsJsonAsync(responseObject, cancellationToken);
        return true;
    }
}
