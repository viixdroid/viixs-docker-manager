using System.Net;
using ViixsDockerManager.Shared.Exceptions;

namespace ViixsDockerManager.DockLight.Exceptions;

public class NoContainersFoundException()
    : ViixDockerManagerWithHttpStatusCodeException("There were no containers found", HttpStatusCode.NotFound)
{
}
