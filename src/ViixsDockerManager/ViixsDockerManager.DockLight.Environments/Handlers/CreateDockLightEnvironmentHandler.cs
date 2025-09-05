using Microsoft.Extensions.Logging;
using ViixsDockerManager.DockLight.Environments.Models.Commands;
using ViixsDockerManager.DockLight.Shared.Entities;
using ViixsDockerManager.Mediator.Commands;
using ViixsDockerManager.Shared.Database.Repositories;

namespace ViixsDockerManager.DockLight.Environments.Handlers;

public class CreateDockLightEnvironmentHandler(ILogger<CreateDockLightEnvironmentHandler> logger, IDatabaseWriteRepository<DockLightEnvironment> databaseWriteRepository) : ICommandHandler<CreateDockLightEnvironmentCommand>
{
    public async Task Handle(CreateDockLightEnvironmentCommand command, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("We handeling command with command {Command}", command);
        var dle = new DockLightEnvironment()
        {
            Name = command.Name,
            ApiLocation = command.ApiLocation
        };

        await databaseWriteRepository.Save(dle);
    }
}
