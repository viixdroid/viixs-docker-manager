namespace ViixsDockerManager.Shared.Database.Exceptions;

public class CannotWriteInAReadContextException()
    : Exception("Cannot write in a read context")
{

}
