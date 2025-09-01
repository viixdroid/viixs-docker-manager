namespace ViixsDockerManager.DockLight.Models;

public enum ContainerState
{
    Unknown,
    Created,
    Running,
    Paused,
    Restarting,
    Exited,
    Removing,
    Dead,
}
