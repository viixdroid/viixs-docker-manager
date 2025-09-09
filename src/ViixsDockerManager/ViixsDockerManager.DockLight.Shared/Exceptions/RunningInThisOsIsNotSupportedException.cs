using System.Net;
using ViixsDockerManager.Shared.Exceptions;

namespace ViixsDockerManager.DockLight.Environments.Exceptions;

public class RunningInThisOsIsNotSupportedException(string osName)
    : ViixDockerManagerWithHttpStatusCodeException($"Running this application on {osName} is not supported.", HttpStatusCode.NotImplemented)
{

}
