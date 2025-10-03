using ViixsDockerManager.DockLight.Environments.Models.Commands;
using ViixsDockerManager.DockLight.Shared.Entities;
using ViixsDockerManager.Mediator.Commands;
using ViixsDockerManager.Shared.Database.Repositories;

namespace ViixsDockerManager.DockLight.Environments.Handlers;

public class CreateDockLightEnvironmentHandler(IDatabaseWriteRepository<DockLightEnvironment> databaseWriteRepository) : ICommandHandler<CreateDockLightEnvironmentCommand>
{
    public Task Handle(CreateDockLightEnvironmentCommand command, CancellationToken cancellationToken = default)
        => databaseWriteRepository.Save((DockLightEnvironment)command);
}
