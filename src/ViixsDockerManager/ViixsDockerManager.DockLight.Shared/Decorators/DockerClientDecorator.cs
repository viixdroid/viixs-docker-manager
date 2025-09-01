using System.Collections.Concurrent;
using System.Reflection;
using Docker.DotNet;
using ViixsDockerManager.DockLight.Shared.Entities;
using ViixsDockerManager.DockLight.Shared.Queries.Filters;
using ViixsDockerManager.DockLight.Shared.Services.Interfaces;
using ViixsDockerManager.Shared.Database.Repositories;
using ViixsDockerManager.Shared.Exceptions;
using ViixsDockerManager.Shared.Helpers;

namespace ViixsDockerManager.DockLight.Shared.Decorators;

public class DockerClientDecorator<TService> : DispatchProxy
{
    private readonly ConcurrentDictionary<MethodInfo, ParameterInfo[]> _methodInfoCache = new ConcurrentDictionary<MethodInfo, ParameterInfo[]>();
    private static readonly ConcurrentDictionary<Type, PropertyInfo[]> _dockerClientTypeCache = new ConcurrentDictionary<Type, PropertyInfo[]>();

    private TService _service;
    private IReadRepository<DocklightEnvironment> _docklightEnvironmentRepository;
    private IDockerClientService _dockerClientService;

    public static TService CreateService(TService serviceToDecorate, IReadRepository<DocklightEnvironment> docklightEnvironmentRepository, IDockerClientService dockerClientService)
    {
        var proxy = Create<TService, DockerClientDecorator<TService>>();
        if (proxy is DockerClientDecorator<TService> dockerClientDecorator)
        {
            dockerClientDecorator.SetParameters(serviceToDecorate, docklightEnvironmentRepository, dockerClientService);
        }

        return proxy;
    }

    private void SetParameters(TService serviceToDecorate, IReadRepository<DocklightEnvironment> docklightEnvironmentRepository, IDockerClientService dockerClientService)
    {
        _service = Guard.ValueIsNotNull(serviceToDecorate, nameof(serviceToDecorate));
        _docklightEnvironmentRepository = Guard.ValueIsNotNull(docklightEnvironmentRepository, nameof(docklightEnvironmentRepository));
        _dockerClientService = Guard.ValueIsNotNull(dockerClientService, nameof(dockerClientService));
    }

    protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
    {
        _ = Guard.ValueIsNotNull(targetMethod, nameof(targetMethod));

        var returnType = targetMethod!.ReturnType;

        if (!typeof(Task).IsAssignableFrom(returnType))
        {
            throw new Exception($"A service in this {nameof(DockerClientDecorator<TService>)} can only have async methods");
            // return targetMethod.InvokeSync(_service, args, onBefore: GetDockerClientEndpoint);
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

        var dockerClientType = typeof(IDockerClient);
        var allowedInterfaceTypes = _dockerClientTypeCache.GetOrAdd(dockerClientType, dockerClientType.GetProperties());

        var parameters = _methodInfoCache.GetOrAdd(methodInfo, methodInfo.GetParameters());

        if (parameters.Length < 2)
        {
            throw new Exception("Expected a method signature with atleast 2 parameters");
        }

        var firstParameter = parameters[0];
        if (firstParameter.ParameterType != typeof(Guid))
        {
            throw new Exception("Expected first parameter to be of type Guid");
        }

        var secondParameter = parameters[1];
        var secondParameterType = secondParameter.ParameterType;

        var endPointParameter = allowedInterfaceTypes.FirstOrDefault(p => p.PropertyType == secondParameterType);

        if (endPointParameter is null)
        {
            //TODO: Typed exception
            throw new Exception("Expected second parameter to be an property in the IDockerClient Interface");
        }

        var dockerClientId = arguments[0];
        if (dockerClientId is not Guid dockerClientGuid)
        {
            throw new Exception("Passed first argument is not a guid");
        }

        var dockerAddress = await _docklightEnvironmentRepository.GetByFilterAsync(new DockLightEnvironmentByEnvironmentIdFilter(dockerClientGuid));
        var dockerClient = _dockerClientService.GetDockerClient(dockerAddress?.ApiLocation);

        var tmp = endPointParameter.GetValue(dockerClient);
        var (endPointArgumentIndex, _) = parameters.Index().SingleOrDefault(p => p.Item.ParameterType == secondParameterType);

        arguments[endPointArgumentIndex] = tmp;
    }
}
