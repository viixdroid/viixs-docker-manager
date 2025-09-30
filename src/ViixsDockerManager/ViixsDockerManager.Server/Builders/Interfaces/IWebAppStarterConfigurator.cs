namespace ViixsDockerManager.Server.Builders.Interfaces;

public interface IWebAppStarterConfigurator : IWebAppStarterEvents
{
    IWebAppStarterConfigurator ConfigureEventHandlers();
    IWebAppStarter CreateWebApplicationBuilder(string[] applicationArgs, Action<WebApplicationBuilder> initializationAction);
}
