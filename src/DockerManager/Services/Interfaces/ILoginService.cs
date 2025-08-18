using DockerManager.Auth.Models.Pages;

namespace DockerManager.Services.Interfaces;

public interface ILoginService
{
    Task<bool> LoginAsync(LoginModel loginModel);
}
