using System.Net;
using ViixsDockerManager.Shared.Exceptions;

namespace ViixsDockerManager.DockLight.Shared.Exceptions;

public class NoContainersFoundException()
    : ViixsDockerManagerWithHttpStatusCodeException("There were no containers found", HttpStatusCode.NotFound)
{
}
