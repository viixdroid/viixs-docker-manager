namespace ViixsDockerManager.Server.Builders.Interfaces;

public interface IStartWebApp
{
    IStartWebApp ConfigureApplication();
    IStartWebApp RunMigrations();
    IStartWebApp ConfigureRoutes();
    Task RunApp(CancellationToken cancellationToken);
}
