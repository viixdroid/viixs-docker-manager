using DockerManager.Auth.Models.Pages;

namespace DockerManager.Services;

public interface ILoginService
{
    Task<bool> LoginAsync(LoginModel loginModel);
}
