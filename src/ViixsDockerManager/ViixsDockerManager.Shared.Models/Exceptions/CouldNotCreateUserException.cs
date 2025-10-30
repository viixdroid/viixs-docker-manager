using System.Net;
using ViixsDockerManager.Shared.Exceptions;
using ViixsDockerManager.Shared.Models.Constants;
using ViixsDockerManager.Shared.Models.Errors;

namespace ViixsDockerManager.Shared.Models.Exceptions;

public class CouldNotCreateUserException(string createUserErrors)
    : ViixsDockerManagerWithHttpStatusCodeException($"Could not create user account: {createUserErrors}", HttpStatusCode.BadRequest)
{
    public IReadOnlyList<ErrorDetail> Errors => [.. IdenityErrorsMap.Errors.Where(errorDetail => createUserErrors.Contains(errorDetail.Code))];
}
