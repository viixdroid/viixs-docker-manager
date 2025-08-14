using DockerManager.Auth.Models.Pages;
using Microsoft.AspNetCore.Components;

namespace DockerManager.Components.Pages.Account;

public partial class Login : ComponentBase
{
    [SupplyParameterFromForm]
    private LoginModel LoginModel { get; set; } = new();
}