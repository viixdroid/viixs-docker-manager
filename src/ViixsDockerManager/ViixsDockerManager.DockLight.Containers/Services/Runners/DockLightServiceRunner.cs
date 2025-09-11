using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Docker.DotNet;
using ViixsDockerManager.DockLight.Services.Interfaces;
using ViixsDockerManager.DockLight.Shared.Entities;
using ViixsDockerManager.DockLight.Shared.Exceptions;
using ViixsDockerManager.DockLight.Shared.Queries.Filters;
using ViixsDockerManager.DockLight.Shared.Services.Interfaces;
using ViixsDockerManager.Shared.Database.Repositories;
using ViixsDockerManager.Shared.Helpers;

namespace ViixsDockerManager.DockLight.Services.Runners;

internal class DockLightServiceRunner : IDockerServiceRunner
{
    private static readonly ConcurrentDictionary<Type, Func<IDockerClient, IDockerService>> _serviceCache = new ConcurrentDictionary<Type, Func<IDockerClient, IDockerService>>();
    private static IDockerClient? _dockerClient = null;

    private readonly IReadRepository<DockLightEnvironment> _docklightEnvironmentRepository;
    private readonly IDockerClientService _dockerClientService;

    public DockLightServiceRunner(IReadRepository<DockLightEnvironment> docklightEnvironmentRepository, IDockerClientService dockerClientService)
    {
        _serviceCache.TryAdd(typeof(IDockerContainerService), (client) => new DockerContainerService(client.Containers));
        _docklightEnvironmentRepository = docklightEnvironmentRepository;
        _dockerClientService = dockerClientService;
    }

    public async Task<TRequestedService> GetServiceAsync<TRequestedService>(Guid environmentId)
        where TRequestedService : class, IDockerService
    {
        if (_dockerClient is null) //TODO: use actual caching per environment
        {
            var dockerEnvironment = await _docklightEnvironmentRepository.GetByFilterAsync(new DockLightEnvironmentByEnvironmentIdFilter(environmentId));
            dockerEnvironment = Guard.ValueIsNotNull(dockerEnvironment, nameof(dockerEnvironment));

            _dockerClient = _dockerClientService.GetDockerClient(dockerEnvironment.ApiLocation);
            _dockerClient = Guard.ValueIsNotNull(_dockerClient, nameof(_dockerClient));
        }

        var service = _serviceCache.TryGetValue(typeof(TRequestedService), out var serviceFactory);
        if (!service)
        {
            throw new /*ServiceNotRegistered*/Exception(typeof(TRequestedService).Name); //TODO: Fix
        }
        serviceFactory = Guard.ValueIsNotNull(serviceFactory, nameof(serviceFactory));

        return (TRequestedService)serviceFactory(_dockerClient);
    }
}
