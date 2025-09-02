using System.Reflection;
using ViixsDockerManager.DockLight.Shared.Decorators.Helpers;
using ViixsDockerManager.DockLight.Shared.Decorators.Providers;
using ViixsDockerManager.Shared.Helpers;

namespace ViixsDockerManager.DockLight.Shared.Decorators;

public class DockerClientDecorator<TService> : DispatchProxy
{
    private TService? _service;
    private IDockerClientEndpointProvider? _dockerClientEndpointProvider;

    public static TService CreateService(TService serviceToDecorate, IDockerClientEndpointProvider dockerClientEndpointProvider)
    {
        var proxy = Create<TService, DockerClientDecorator<TService>>();
        if (proxy is DockerClientDecorator<TService> dockerClientDecorator)
        {
            dockerClientDecorator.SetParameters(serviceToDecorate, dockerClientEndpointProvider);
        }

        return proxy;
    }

    private void SetParameters(TService serviceToDecorate, IDockerClientEndpointProvider dockerClientEndpointProvider)
    {
        _service = Guard.ValueIsNotNull(serviceToDecorate, nameof(serviceToDecorate));
        _dockerClientEndpointProvider = Guard.ValueIsNotNull(dockerClientEndpointProvider, nameof(dockerClientEndpointProvider));
    }

    protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
    {
        _ = Guard.ValueIsNotNull(targetMethod, nameof(targetMethod));

        var returnType = targetMethod!.ReturnType;

        if (!typeof(Task).IsAssignableFrom(returnType))
        {
            throw new Exception($"A service in this {nameof(DockerClientDecorator<TService>)} can only have async methods");
        }

        if (returnType == typeof(Task))
        {
            return targetMethod.InvokeAsAsync(_service, args, onBefore: GetDockerClientEndpoint);
        }

        return targetMethod.InvokeAsAsyncWithResult(_service, args, onBefore: GetDockerClientEndpoint);
    }

    private async Task GetDockerClientEndpoint(MethodInfo? methodInfo, object?[]? arguments)
    {
        methodInfo = Guard.ValueIsNotNull(methodInfo, nameof(methodInfo));

        arguments = Guard.ValueIsNotNull(arguments, nameof(arguments));
        await methodInfo.PopulateDockerClientEndpoint(arguments, _dockerClientEndpointProvider!);
    }
}
