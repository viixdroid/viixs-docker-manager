using ViixsDockerManager.Shared.Exceptions;

namespace ViixsDockerManager.Shared.Helpers;

public static class Guard
{
    public static TValue ValueIsNotNull<TValue>(TValue? value, string parameterName)
    {
        ViixsDockerManagerException.ThrowIfArgumentNull(value, parameterName);
        return value!;
    }

    public static string ValueIsNotNullOrEmpty(string? value, string parameterName)
    {
        if(string.IsNullOrEmpty(value))
        {
            throw new ViixsDockerManagerException($"Parameter '{parameterName}' cannot be null or empty.");
        }

        return value;
    }
}
