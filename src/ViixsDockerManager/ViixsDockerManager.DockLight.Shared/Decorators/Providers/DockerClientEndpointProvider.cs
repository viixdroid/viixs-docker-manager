using System.Collections.Concurrent;
using System.Reflection;
using Docker.DotNet;
using ViixsDockerManager.DockLight.Shared.Entities;
using ViixsDockerManager.DockLight.Shared.Exceptions;
using ViixsDockerManager.DockLight.Shared.Queries.Filters;
using ViixsDockerManager.DockLight.Shared.Services.Interfaces;
using ViixsDockerManager.Shared.Database.Repositories;
using ViixsDockerManager.Shared.Helpers;

namespace ViixsDockerManager.DockLight.Shared.Decorators.Providers;

public class DockerClientEndpointProvider(
    IReadRepository<DocklightEnvironment> docklightEnvironmentRepository,
    IDockerClientService dockerClientService)
    : IDockerClientEndpointProvider
{
    private readonly IReadRepository<DocklightEnvironment> _docklightEnvironmentRepository = Guard.ValueIsNotNull(docklightEnvironmentRepository, nameof(docklightEnvironmentRepository));
    private readonly IDockerClientService _dockerClientService = Guard.ValueIsNotNull(dockerClientService, nameof(dockerClientService));

    private static readonly ConcurrentDictionary<Type, PropertyInfo[]> _dockerClientTypeCache = new ConcurrentDictionary<Type, PropertyInfo[]>();
    private static readonly ConcurrentDictionary<MethodInfo, ParameterInfo[]> _methodInfoCache = new ConcurrentDictionary<MethodInfo, ParameterInfo[]>();

    public async Task<object?> ProvideDockerClientEndpoint(MethodInfo methodInfo, object?[] arguments)
    {
        methodInfo = Guard.ValueIsNotNull(methodInfo, nameof(methodInfo));
        arguments = Guard.ValueIsNotNull(arguments, nameof(arguments));

        var methodParameters = GetMethodParameters(methodInfo);

        var dockerClientProperties = _dockerClientTypeCache.GetOrAdd(typeof(IDockerClient), typeof(IDockerClient).GetProperties());
        var endpointProperty = ValidateParameterTypesAndGetEndpointProperty(methodInfo, methodParameters, dockerClientProperties);

        var dockerClientGuid = ValidateRuntimeArgumentsAndGetDockerClientId(methodInfo, arguments);

        var dockerClient = await GetDockerClientInstance(dockerClientGuid, methodInfo.Name);

        var endpointValue = GetEndpointValue(dockerClient, endpointProperty);

        return endpointValue;
    }

    private ParameterInfo[] GetMethodParameters(MethodInfo methodInfo)
    {
        const int expectedParameters = 2;
        var methodParameters = _methodInfoCache.GetOrAdd(methodInfo, methodInfo.GetParameters());

        if (methodParameters.Length < expectedParameters)
        {
            throw new InvalidMethodSignatureException(methodInfo.Name, expectedParameters, methodParameters.Length);
        }

        return methodParameters;
    }

    private static PropertyInfo ValidateParameterTypesAndGetEndpointProperty(
        MethodInfo methodInfo, ParameterInfo[] methodParameters, PropertyInfo[] dockerClientProperties)
    {
        var firstParameter = methodParameters[0];
        if (firstParameter.ParameterType != typeof(Guid))
        {
            throw new ParameterTypeMismatchException(firstParameter.Name ?? "Unknown parameter name", typeof(Guid), firstParameter.ParameterType);
        }

        var secondParameter = methodParameters[1];
        var targetEndpointType = secondParameter.ParameterType;

        var endpointProperty = dockerClientProperties.FirstOrDefault(p => p.PropertyType == targetEndpointType);

        return endpointProperty ?? throw new MissingDockerClientEndpointPropertyException(methodInfo.Name, targetEndpointType.Name, nameof(IDockerClient));
    }

    private static Guid ValidateRuntimeArgumentsAndGetDockerClientId(MethodInfo methodInfo, object?[] actualArguments)
    {
        if (actualArguments.Length == 0 || actualArguments[0] is null)
        {
            throw new InvalidArgumentTypeException($"First argument for method '{methodInfo.Name}'", typeof(Guid), null);
        }

        var dockerClientIdArgument = actualArguments[0];
        if (dockerClientIdArgument is not Guid dockerClientGuid)
        {
            throw new InvalidArgumentTypeException($"First argument for method '{methodInfo.Name}'", typeof(Guid), dockerClientIdArgument?.GetType());
        }

        return dockerClientGuid;
    }

    private async Task<IDockerClient> GetDockerClientInstance(Guid dockerClientGuid, string methodName)
    {
        var dockerEnvironment = await _docklightEnvironmentRepository.GetByFilterAsync(new DockLightEnvironmentByEnvironmentIdFilter(dockerClientGuid));

        if (dockerEnvironment?.ApiLocation is null)
        {
            throw new DockerClientInitializationException(
                $"Could not resolve Docker API location for client ID '{dockerClientGuid}' (method: '{methodName}'). The environment might not exist or its API location is missing in the repository.");
        }

        var dockerClient = _dockerClientService.GetDockerClient(dockerEnvironment.ApiLocation);
        if (dockerClient is null)
        {
            throw new DockerClientInitializationException(
                $"_dockerClientService.GetDockerClient returned null for API location '{dockerEnvironment.ApiLocation}' (method: '{methodName}'). This indicates a failure to create a Docker client instance.");
        }

        return dockerClient;
    }

    private static object? GetEndpointValue(IDockerClient dockerClient, PropertyInfo endpointProperty)
    {
        object? endpointValue;
        try
        {
            endpointValue = endpointProperty.GetValue(dockerClient);
        }
        catch (Exception ex)
        {
            throw new DockerClientEndpointResolutionException(
                $"Failed to retrieve value for property '{endpointProperty.Name}' (type: {endpointProperty.PropertyType.Name}) from Docker client instance. This might indicate an issue with the IDockerClient implementation.", ex);
        }

        return endpointValue;
    }
}
