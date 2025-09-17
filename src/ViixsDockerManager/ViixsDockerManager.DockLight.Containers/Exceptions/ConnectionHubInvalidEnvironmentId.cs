namespace ViixsDockerManager.DockLight.Exceptions;

public class ConnectionHubInvalidEnvironmentId(string environmentId)
    : Exception($"Environment Id {environmentId ?? "<Empty EnvironmentId>"} is not valid and cannot be used for real time connections. Did you add '?environmentId=<actual environment id>' to you url?")
{
}
