using Microsoft.AspNetCore.Components;

namespace DockerManager.Shared.Views.Components;

public partial class GoToLoginButton(NavigationManager navigationManager) : ComponentBase
{
    private void NavigateToLogin() => navigationManager.NavigateTo("/Account/Login");
}

