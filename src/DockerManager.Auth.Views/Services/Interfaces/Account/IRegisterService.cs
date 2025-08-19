using DockerManager.Auth.Models.Pages;

namespace DockerManager.Auth.Views.Services.Interfaces.Account;

public interface IRegisterService
{
    /// <summary>
    /// Calls the Register api, to register a new user.
    ///
    /// </summary>
    /// <param name="model">
    /// The registration model containing user details such as email and password.
    /// </param>
    /// <returns>
    /// A tuple containing a boolean indicating success or failure, and a string with an error message if applicable.
    /// </returns>
    Task<(bool, string)> RegisterAsync(RegisterModel model);
}
