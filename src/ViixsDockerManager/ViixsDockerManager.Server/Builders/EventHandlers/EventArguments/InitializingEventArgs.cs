namespace ViixsDockerManager.Server.Builders.EventHandlers.EventArguments;

public class InitializingEventArgs(IConfiguration configuration) : EventArgs
{
    public IConfiguration Configuration { get; } = configuration;
}
