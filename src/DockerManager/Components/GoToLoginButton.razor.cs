using Microsoft.AspNetCore.Components;

namespace DockerManager.Components;

public partial class GoToLoginButton(NavigationManager navigationManager) : ComponentBase
{
    private void NavigateToLogin() => navigationManager.NavigateTo("/Account/Login");
}

