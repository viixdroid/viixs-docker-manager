using DockerManager.Auth.Models.Pages;
using DockerManager.Auth.Views.Services.Interfaces.Account;
using Microsoft.AspNetCore.Components;

namespace DockerManager.Auth.Views.Frontend;

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
