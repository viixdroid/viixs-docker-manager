namespace ViixsDockerManager.Setup.Exceptions;

public class SetupIdDoesNotExistException(Guid setupId)
    : Exception($"The given setupId {setupId} does not exists. Please start a new setup flow through '/setup/start'")
{
}
