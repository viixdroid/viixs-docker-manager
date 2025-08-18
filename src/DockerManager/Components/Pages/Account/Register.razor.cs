using DockerManager.Auth.Models.Pages;
using DockerManager.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace DockerManager.Components.Pages.Account;

public partial class Register(IRegisterService registerService, NavigationManager navigationManager) : ComponentBase
{
    [SupplyParameterFromForm]
    private RegisterModel RegisterModel { get; set; } = new();

    private string ErrorMessage { get; set; } = "";

    public async Task RegisterUserAsync(EditContext editContext)
    {
        var (registrationSuccess, registrationMessage) = await registerService.RegisterAsync(RegisterModel);

        if (registrationSuccess)
        {
            navigationManager.NavigateTo(registrationMessage);
        }

        ErrorMessage = registrationMessage;
    }
}