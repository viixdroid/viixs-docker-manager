using ViixsDockerManager.Shared.Helpers;

namespace ViixsDockerManager.Mediator.Helpers;

internal static class GenericNameHelpers
{
    public static string? GetName(Type type)
    {
        type = Guard.ValueIsNotNull(type, nameof(type));
        return type!.FullName;
    }

    public static string? GetName<TType>()
    {
        return GetName(typeof(TType));
    }
}
