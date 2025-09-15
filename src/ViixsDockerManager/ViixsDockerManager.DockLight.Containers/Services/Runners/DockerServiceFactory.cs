using System.Collections.Concurrent;
using Docker.DotNet;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using ViixsDockerManager.DockLight.Exceptions;
using ViixsDockerManager.DockLight.Services.Interfaces;
using ViixsDockerManager.DockLight.Shared.Entities;
using ViixsDockerManager.DockLight.Shared.Queries.Filters;
using ViixsDockerManager.DockLight.Shared.Services.Interfaces;
using ViixsDockerManager.Shared.Database.Repositories;
using ViixsDockerManager.Shared.Decorators;
using ViixsDockerManager.Shared.Helpers;

namespace ViixsDockerManager.DockLight.Services.Runners;

internal class DockerServiceFactory : IDockerServiceFactory
{
    private static readonly ConcurrentDictionary<Type, Func<IDockerClient, IDockerService>> _serviceFactoryCache = new ConcurrentDictionary<Type, Func<IDockerClient, IDockerService>>();

    private readonly IMemoryCache _memoryCache;

    private readonly IDatabaseReadRepository<DockLightEnvironment> _docklightEnvironmentRepository;
    private readonly IDockerClientService _dockerClientService;

    public DockerServiceFactory(IDatabaseReadRepository<DockLightEnvironment> docklightEnvironmentRepository, IDockerClientService dockerClientService, IMemoryCache memoryCache, ILoggerFactory loggerFactory)
    {
        _memoryCache = Guard.ValueIsNotNull(memoryCache, nameof(memoryCache));
        _docklightEnvironmentRepository = docklightEnvironmentRepository;
        _dockerClientService = dockerClientService;
        PopulateServiceFactoryCache(loggerFactory);
    }

    Task<TRequestedService> IDockerServiceFactory.GetServiceAsync<TRequestedService>(Guid environmentId)
        => GetOrCreatedService<TRequestedService>(environmentId);

    private static void PopulateServiceFactoryCache(ILoggerFactory loggerFactory)
    {
        ArgumentNullException.ThrowIfNull(loggerFactory);
        _serviceFactoryCache.TryAdd(typeof(IDockerContainerService), (client) => ViixsServiceExceptionHandler<IDockerContainerService>.CreateService(new DockerContainerService(client.Containers), loggerFactory.CreateLogger<DockerContainerService>()));
        _serviceFactoryCache.TryAdd(typeof(IDockerImageService), (client) => ViixsServiceExceptionHandler<IDockerImageService>.CreateService(new DockerImageService(client.Images), loggerFactory.CreateLogger<DockerImageService>()));
        _serviceFactoryCache.TryAdd(typeof(IDockerSystemService), (client) => ViixsServiceExceptionHandler<IDockerSystemService>.CreateService(new DockerSystemService(client.System), loggerFactory.CreateLogger<DockerSystemService>()));
    }

    private async Task<TRequestedService> GetOrCreatedService<TRequestedService>(Guid environmentId)
        where TRequestedService : class, IDockerService
    {
        var cachedDockerClient = await GetOrCreateDockerClient(environmentId).ConfigureAwait(false);
        cachedDockerClient = Guard.ValueIsNotNull(cachedDockerClient, nameof(cachedDockerClient));

        var service = _serviceFactoryCache.TryGetValue(typeof(TRequestedService), out var serviceFactory);
        if (!service)
        {
            throw new ServiceNotRegisteredException(typeof(TRequestedService));
        }
        serviceFactory = Guard.ValueIsNotNull(serviceFactory, nameof(serviceFactory));
        return (TRequestedService)serviceFactory(cachedDockerClient!);
    }

    private Task<IDockerClient?> GetOrCreateDockerClient(Guid environmentId)
    {
        return _memoryCache.GetOrCreateAsync(environmentId, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(120);
            IDockerClient? dockerClient = null;
            var dockerEnvironment = await _docklightEnvironmentRepository.GetByFilterAsync(new DockLightEnvironmentByEnvironmentIdFilter(environmentId));
            dockerEnvironment = Guard.ValueIsNotNull(dockerEnvironment, nameof(dockerEnvironment));

            dockerClient = _dockerClientService.GetDockerClient(dockerEnvironment.ApiLocation);
            dockerClient = Guard.ValueIsNotNull(dockerClient, nameof(dockerClient));
            return dockerClient;
        });
    }
}
