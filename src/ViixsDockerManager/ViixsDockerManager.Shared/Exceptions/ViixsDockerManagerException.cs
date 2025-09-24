using ViixsDockerManager.Shared.Helpers;

namespace ViixsDockerManager.Shared.Exceptions;

public class ViixsDockerManagerException : Exception
{
    public ViixsDockerManagerException(string message)
        : base(message)
    {
    }

    public ViixsDockerManagerException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public ViixsDockerManagerException(Exception exception)
        : base(exception.Message, exception)
    {
    }

    public static ViixsDockerManagerException InvalidOperation(string message)
        => new ViixsDockerManagerException(message, new InvalidOperationException(message));

    public static ViixsDockerManagerException InvalidOperation(InvalidOperationException invalidOperationException)
        => new ViixsDockerManagerException(invalidOperationException);

    public static ViixsDockerManagerException ServiceException(string message, Exception innerException)
        => new ViixsDockerManagerException(message, innerException);

    public static void ThrowIfArgumentNull<TValue>(TValue? value, string? argumentName)
    {
        if (value is null)
        {
            throw new ViixsDockerManagerException($"{argumentName} is null");
        }
    }
}
