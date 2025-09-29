namespace ViixsDockerManager.Server.Builders;

public interface IStartWebApp
{
    IStartWebApp ConfigureApplication();
    IStartWebApp RunMigrations();
    Task RunApp(CancellationToken cancellationToken);
}
