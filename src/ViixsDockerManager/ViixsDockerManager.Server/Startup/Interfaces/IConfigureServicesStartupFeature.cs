namespace ViixsDockerManager.Server.Startup.Interfaces;

public interface IConfigureServicesStartupFeature
{
    void ConfigureServices(WebApplicationBuilder webAppbuilder);
}
