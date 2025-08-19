using DockerManager.Auth.Models.Pages;

namespace DockerManager.Auth.Services.Interfaces.Account;

public interface ILoginService
{
    Task<bool> LoginAsync(LoginModel loginModel);
}
