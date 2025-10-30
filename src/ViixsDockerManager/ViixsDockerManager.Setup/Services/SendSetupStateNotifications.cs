using Microsoft.AspNetCore.SignalR;
using ViixsDockerManager.Setup.Controllers.Hubs;
using ViixsDockerManager.Setup.Models.Dtos;
using ViixsDockerManager.Setup.Services.Interfaces;
using ViixsDockerManager.Shared.Models.Errors;
using ViixsDockerManager.Shared.WebSockets.Interfaces;

namespace ViixsDockerManager.Setup.Services;

internal class SendSetupStateNotifications(
    IHubContext<SetupInformationHub, ISetupInformationContext> hubContext,
    IConnectionRegistery<SetupInformationHub> connectionRegistery) : ISendSetupStateNotifications
{
    private const string DefaultSetupIdKey = "client-setup-id-key";

    private ISetupInformationContext? GetClient()
    {
        var setupId = connectionRegistery.GetConnectionId(DefaultSetupIdKey);
        if (setupId == null)
        {
            return null;
        }
        return hubContext.Clients.Group(setupId);
    }

    public async Task SendOnSetupStartedAsync(string connectionId, SetupStep setupStep)
    {
        if (string.IsNullOrEmpty(connectionId))
        {
            return;
        }

        var setupId = setupStep.SetupId.ToString();

        // Update the connection registry with the default setup ID key
        connectionRegistery.AddOrUpdateConnectionId(DefaultSetupIdKey, setupId);

        await hubContext.Groups.AddToGroupAsync(connectionId, setupId);
        await hubContext.Clients.Group(setupId).OnSetupStarted(setupStep);
    }

    public async Task SendOnUserCreationFailedAsync(IReadOnlyList<ErrorDetail> errorDetails)
    {
        var client = GetClient();
        if (client is null)
        {
            return;
        }
        await client.OnUserCreationFailed([.. errorDetails]);
    }

    public async Task SendOnEnvironmentCreated(SetupStep setupStep, object validationInformation)
    {
        var client = GetClient();
        if (client is null)
        {
            return;
        }
        await client.OnEnvironmentCreated(setupStep);
    }

    public async Task SendOnSetupFinishedAsync(SetupStep setupStep)
    {
        var client = GetClient();
        if (client is null)
        {
            return;
        }
        await client.OnSetupFinished(setupStep);
    }

    public async Task SendNextSetupStepAsync(SetupStep setupStep)
    {
        var client = GetClient();
        if (client is null)
        {
            return;
        }
        await client.OnNextSetupStep(setupStep);
    }
}
