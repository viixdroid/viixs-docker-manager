using Microsoft.Extensions.Hosting;
using ViixsDockerManager.Shared.Startup.Interfaces;

namespace ViixsDockerManager.Shared.AspNet.Startup.Interfaces;

public interface IHostComponent : IApplicationBuilderComponent
{
    void ConfigureHost(IHostBuilder hostBuilder);
}
