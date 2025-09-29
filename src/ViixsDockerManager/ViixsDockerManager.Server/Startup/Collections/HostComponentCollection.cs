using ViixsDockerManager.Server.Components;
using ViixsDockerManager.Server.Startup.Builders;
using ViixsDockerManager.Shared.AspNet.Startup.Interfaces;

namespace ViixsDockerManager.Server.Startup.Collections;

internal class HostComponentCollection(IConfiguration configuration) : BuilderCollection<IHostComponent>
{
    protected override IReadOnlyList<IHostComponent> GetComponents()
        => [
            new LoggingConfigurationHostComponent()            
            ];
}
