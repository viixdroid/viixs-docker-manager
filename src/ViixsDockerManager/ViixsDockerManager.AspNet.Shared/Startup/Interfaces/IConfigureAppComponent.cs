using Microsoft.AspNetCore.Builder;
using ViixsDockerManager.Shared.Startup.Interfaces;

namespace ViixsDockerManager.Shared.AspNet.Startup.Interfaces;

public interface IConfigureAppComponent : IApplicationBuilderComponent
{
    void ConfigureApplication(WebApplication webApplication);
}
