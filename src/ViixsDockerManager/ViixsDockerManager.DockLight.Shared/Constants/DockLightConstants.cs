namespace ViixsDockerManager.DockLight.Shared.Constants;

public static class DockLightConstants
{
    /// <summary>
    /// According to several internet sources, when dotnet builds, it adds this variable. With this we can now see if
    /// we are running in a docker environment or just on a host like Windows, *Unix or macOS.
    /// </summary>
    public const string DotnetRunningInContainer = "DOTNET_RUNNING_IN_CONTAINER";

    public static class Unix
    {
        public const string Protocol = "unix";
        public const string Socket = @"/var/run/docker.sock";
    }

    public static class Windows
    {
        public const string Protocol = "npipe";
        public const string Npipe = @"/pipe/docker_engine";
    }

    public static class Tcp
    {
        public const string Protocol = "tcp";
    }
}
