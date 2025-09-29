using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using ViixsDockerManager.Shared.AspNet.Startup.Interfaces;

namespace ViixsDockerManager.Shared.AspNet.Startup;

public abstract class ConfigureAppComponent : IConfigureAppComponent
{
    protected abstract void ConfigureApplication(WebApplication webApplication);

    void IConfigureAppComponent.ConfigureApplication(WebApplication webApplication) => ConfigureApplication(webApplication);
}
