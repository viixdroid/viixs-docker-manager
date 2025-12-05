using ViixsDockerManager.Mediator.Queries;
using ViixsDockerManager.Setup.Models.Dtos;
using ViixsDockerManager.Setup.Models.Queries;
using ViixsDockerManager.Setup.Services;
using ViixsDockerManager.Shared.Attributes;

namespace ViixsDockerManager.Setup.Handlers.Queries;

[ViixsController(Action = "isSetupFinished", ControllerName = "SetupRoute2")]
internal sealed class IsSetupFinishedHandler(ISetupService setupService) : IQueryHandler<IsSetupFinishedQuery, IsSetupFinished>
{
    public async Task<IsSetupFinished> Execute(IsSetupFinishedQuery query, CancellationToken cancellationToken = default)
    {
        var isSetupFinished = await setupService.IsSetupFinished();
        return isSetupFinished;
    }
}
