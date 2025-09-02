using System.Reflection;

namespace ViixsDockerManager.DockLight.Shared.Decorators.Providers;

public interface IDockerClientEndpointProvider
{

    Task<object?> ProvideDockerClientEndpoint(
        MethodInfo methodInfo,
        object?[] arguments);
}
