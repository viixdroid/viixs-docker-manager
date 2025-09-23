using System.Net;
using Microsoft.Extensions.Logging;

namespace ViixsDockerManager.Shared.Exceptions;

public abstract class ViixsDockerManagerWithHttpStatusCodeException : Exception
{
    public HttpStatusCode StatusCode { get; }

    protected ViixsDockerManagerWithHttpStatusCodeException(string message,
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError, Exception? innerException = null)
        : base(message, innerException)
    {
        StatusCode = statusCode;
    }

    public void Log(ILogger logger, LogLevel level = LogLevel.Error) => LogException(logger, level);

    /// <summary>
    /// Override this method to add a custom message for the exception when logging
    ///
    /// Will default to writing the exception message and the exception itself to the log.
    /// </summary>
    /// <param name="logger">
    /// The logger which will write the log. Must always be supplied
    /// </param>
    /// <param name="level">
    /// The level to log. By default <see cref="LogLevel.Error"/>.
    ///
    /// This parameter is optional to use.
    ///
    /// If ignored, you can also use the extensions methods on ILogger (i.e. logger.LogError)
    /// </param>
    protected virtual void LogException(ILogger logger, LogLevel level = LogLevel.Error)
    {
        logger.Log(level, this, "{Message}", Message);
    }
}
