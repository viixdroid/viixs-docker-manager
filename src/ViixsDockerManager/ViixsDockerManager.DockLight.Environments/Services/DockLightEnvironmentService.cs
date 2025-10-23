using ViixsDockerManager.DockLight.Shared.Entities;
using ViixsDockerManager.Shared.Database.Repositories;
using ViixsDockerManager.Shared.Helpers;
using ViixsDockerManager.Shared.Models.Commands.DockLightEnvironments;
using ViixsDockerManager.Shared.Services;

namespace ViixsDockerManager.DockLight.Environments.Services;

internal class DockLightEnvironmentService(IDatabaseWriteRepository<DockLightEnvironment> databaseWriteRepository)
    : IDockLightEnvironmentService, ISharedDockLightEnvironmentService
{
    public async Task CreateDockLightEnvironment(ICreateDockLightEnvironmentCommand createDockLightEnvironmentCommand)
    {
        createDockLightEnvironmentCommand = Guard.ValueIsNotNull(createDockLightEnvironmentCommand, nameof(createDockLightEnvironmentCommand));

        // Should validate properties

        var dockLightEnvironmentEntity = new DockLightEnvironment()
        {
            Name = createDockLightEnvironmentCommand.Name,
            ApiLocation = createDockLightEnvironmentCommand.ApiLocation,
        };

        await databaseWriteRepository.Save(dockLightEnvironmentEntity);
    }
}
