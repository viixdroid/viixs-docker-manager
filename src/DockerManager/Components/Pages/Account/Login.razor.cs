using DockerManager.Auth.Models.Pages;
using DockerManager.Services;
using Microsoft.AspNetCore.Components;

namespace DockerManager.Components.Pages.Account;

public partial class Login(ILoginService loginService, NavigationManager navigationManager) : ComponentBase
{
    [SupplyParameterFromForm]
    private LoginModel LoginModel { get; set; } = new();

    private async Task LoginAsync()
    {
        var result = await loginService.LoginAsync(LoginModel);
        if (result)
        {
            navigationManager.NavigateTo("/");
        }
    }
}
