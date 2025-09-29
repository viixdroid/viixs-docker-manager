namespace ViixsDockerManager.Server.Startup.Interfaces;

public interface IConfigureHostStartupFeature
{
    void ConfigureHost(WebApplicationBuilder webAppbuilder);
}
