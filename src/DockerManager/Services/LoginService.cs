using DockerManager.Auth.Models.Pages;
using DockerManager.Auth.Models.Responses;
using Microsoft.AspNetCore.Components;

namespace DockerManager.Services;

internal class LoginService(HttpClient httpClient, NavigationManager navigationManager)
    : AccountBaseService(httpClient, navigationManager)
{
    public async Task<bool> LoginAsync(LoginModel loginModel)
    {
        var response = await httpClient.PostAsJsonAsync("api/Account/Login", loginModel);
        var content = await response.Content.ReadFromJsonAsync<ResponseObject<UserLoggedInModel>>();

        if (content is not null && content.IsSuccess)
        {
            var returnUrl = await response.Content.ReadAsStringAsync();
            // navigationManager.NavigateTo(returnUrl);
            return true;
        }

        //TODO: Log errors? Handle errors ?
        return false;
        // var errorMessage = await response.Content.ReadAsStringAsync();
        // return (false, errorMessage);
    }
}