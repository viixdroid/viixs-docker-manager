namespace ViixsDockerManager.Server.Builders;

public interface ICreateWebApplicationBuilder
{
    IWebAppStarter CreateWebApplicationBuilder(string[] applicationArgs, Action<WebApplicationBuilder> initializationAction);
}
