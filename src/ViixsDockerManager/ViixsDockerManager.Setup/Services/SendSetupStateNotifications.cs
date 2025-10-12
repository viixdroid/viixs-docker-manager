using Microsoft.AspNetCore.SignalR;
using ViixsDockerManager.Setup.Controllers.Hubs;
using ViixsDockerManager.Setup.Models.Dtos;
using ViixsDockerManager.Setup.Services.Interfaces;
using ViixsDockerManager.Shared.WebSockets.Interfaces;

namespace ViixsDockerManager.Setup.Services;

internal class SendSetupStateNotifications(
    IHubContext<SetupInformationHub, ISetupInformationContext> hubContext,
    IConnectionRegistery<SetupInformationHub> connectionRegistery) : ISendSetupStateNotifications
{
    private ISetupInformationContext? GetClient(Guid setupId)
    {
        var connectionId = connectionRegistery.GetConnectionId(setupId.ToString());
        if (connectionId == null)
        {
            return null;
        }
        return hubContext.Clients.Client(connectionId);
    }

    public async Task SendOnSetupStartedAsync(string connectionId, SetupStep setupStep)
    {
        var setupId = setupStep.SetupId.ToString();

        if (string.IsNullOrEmpty(connectionId))
        {
            return;
        }

        connectionRegistery.AddOrUpdateConnectionId(setupId, connectionId);

        await hubContext.Clients.Client(connectionId).OnSetupStarted(setupStep);
    }

    public async Task SendOnUserCreatedAsync(SetupStep setupStep, object validationInformation)//TODO: actual have validation information
    {
        var client = GetClient(setupStep.SetupId);
        if (client is null)
        {
            return;
        }
        await client.OnUserCreated(setupStep);
    }

    public async Task SendOnEnvironmentCreated(SetupStep setupStep, object validationInformation)
    {
        var client = GetClient(setupStep.SetupId);
        if (client is null)
        {
            return;
        }
        await client.OnEnvironmentCreated(setupStep);
    }

    public async Task SendOnSetupFinishedAsync(SetupStep setupStep)
    {
        var client = GetClient(setupStep.SetupId);
        if (client is null)
        {
            return;
        }
        await client.OnSetupFinished(setupStep);
    }


}
