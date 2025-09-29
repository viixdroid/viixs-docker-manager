using Microsoft.Extensions.Hosting;
using ViixsDockerManager.Shared.AspNet.Startup.Interfaces;

namespace ViixsDockerManager.Shared.AspNet.Startup;

public abstract class HostComponent : IHostComponent
{
    protected abstract void ConfigureHost(IHostBuilder hostBuilder);
    void IHostComponent.ConfigureHost(IHostBuilder hostBuilder) => ConfigureHost(hostBuilder);
}
