using System.Reflection;
using ViixsDockerManager.DockLight.Shared.Decorators.Providers;
using ViixsDockerManager.DockLight.Shared.Exceptions;
using ViixsDockerManager.Shared.Helpers;

namespace ViixsDockerManager.DockLight.Shared.Decorators.Helpers;

internal static class DockerClientDecoratorHelpers
{
    /// <summary>
    /// Populates a Docker Client as the second parameter of a method
    /// </summary>
    /// <param name="methodInfo"></param>
    /// <param name="arguments"></param>
    /// <param name="provider"></param>
    public static async Task PopulateDockerClientEndpoint(
        this MethodInfo methodInfo,
        object?[] arguments,
        IDockerClientEndpointProvider provider)
    {
        methodInfo = Guard.ValueIsNotNull(methodInfo, nameof(methodInfo));
        arguments = Guard.ValueIsNotNull(arguments, nameof(arguments));
        provider = Guard.ValueIsNotNull(provider, nameof(provider));

        var endpointValue = await provider.ProvideDockerClientEndpoint(methodInfo, arguments);

        arguments[1] = endpointValue;
    }
}
