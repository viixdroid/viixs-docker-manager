using Microsoft.AspNetCore.SignalR;
using ViixsDockerManager.Shared.WebSockets.Interfaces;

namespace ViixsDockerManager.Setup.Controllers.Hubs;

internal class SetupInformationHub() : Hub<ISetupInformationContext>, IWebSocketHub
{
}
