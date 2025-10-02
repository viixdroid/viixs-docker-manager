using System.Net;
using ViixsDockerManager.Shared.Exceptions;

namespace ViixsDockerManager.Users.Accounts.Exceptions;

public class CouldNotCreateUserException(string createUserErrors)
    : ViixsDockerManagerWithHttpStatusCodeException($"Could not create user account: {createUserErrors}", HttpStatusCode.BadRequest)
{
}
