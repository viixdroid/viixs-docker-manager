namespace ViixsDockerManager.DockLight.Environments.Constants;

public static class DockLightEnvironmentConstants
{
    public class Protocol
    {
        public const string LinuxDockerEngine = "unix://var/run/docker.sock";
        public const string WindowsDockerEngine = "npipe://./pipe/docker_engine";
    }
}
