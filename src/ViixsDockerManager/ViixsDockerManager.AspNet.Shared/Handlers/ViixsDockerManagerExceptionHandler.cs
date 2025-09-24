using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ViixsDockerManager.Shared.Exceptions;
using ViixsDockerManager.Shared.Models;

namespace ViixsDockerManager.Shared.AspNet.Handlers;

public class ViixsDockerManagerExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        var message = "An unhandled exception occurred on the server";
        if (exception is ViixsDockerManagerWithHttpStatusCodeException viixsDockerManagerWithHttpStatusCodeException)
        {
            statusCode = viixsDockerManagerWithHttpStatusCodeException.StatusCode;
            message = viixsDockerManagerWithHttpStatusCodeException.Message;
        }

        httpContext.Response.StatusCode = (int)statusCode;
        var responseObject = ResponseObject.Failure(message);
        await httpContext.Response.WriteAsJsonAsync(responseObject, cancellationToken);
        return true;
    }
}
