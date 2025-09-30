namespace ViixsDockerManager.Server.Startup.Interfaces;

public interface IConfigureRoutesStartupFeature
{
    void ConfigureRoutes(IEndpointRouteBuilder webApp);
}
