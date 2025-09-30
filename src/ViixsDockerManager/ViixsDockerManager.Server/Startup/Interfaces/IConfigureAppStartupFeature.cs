namespace ViixsDockerManager.Server.Startup.Interfaces;

public interface IConfigureAppStartupFeature
{
    void ConfigureApplication(WebApplication webApp);
}
