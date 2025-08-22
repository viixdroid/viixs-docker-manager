using ViixsDockerManager.Shared.Exceptions;

namespace ViixsDockerManager.Shared.Helpers;

public static class Guard
{
    public static TValue ValueIsNotNull<TValue>(TValue? value, string parameterName)
        where TValue : class
    {
        ViixsDockerManagerException.ThrowIfArgumentNull(value, parameterName);
        return value!;
    }
}
