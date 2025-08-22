using ViixsDockerManager.Shared.Helpers;

namespace ViixsDockerManager.Shared.Exceptions;

public class ViixsDockerManagerException : Exception
{
    private ViixsDockerManagerException(string message, Exception innerException) : base(message, innerException)
    {
    }

    private ViixsDockerManagerException(Exception exception) : base(exception.Message, exception)
    {
    }

    public static ViixsDockerManagerException InvalidOperation(string message)
    {
        return new ViixsDockerManagerException(message, new InvalidOperationException(message));
    }

    public static ViixsDockerManagerException InvalidOperation(InvalidOperationException invalidOperationException)
    {
        return new ViixsDockerManagerException(invalidOperationException);
    }

    public static void ThrowIfArgumentNull<TValue>(TValue? value, string? argumentName)
    {
        ArgumentNullException.ThrowIfNull(argumentName);
    }
}
