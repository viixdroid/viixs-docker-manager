using System.Net.Http.Json;
using DockerManager.Auth.Models.Pages;
using DockerManager.Auth.Models.Responses;
using DockerManager.Auth.Providers;
using DockerManager.Auth.Services.Interfaces.Account;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

using static DockerManager.Shared.Constants.ApplicationConstants.Authentication;

namespace DockerManager.Auth.Services.Account;

internal class LoginService(
    IHttpClientFactory httpClientFactory,
    NavigationManager navigationManager,
    ProtectedLocalStorage protectedLocalStorage,
    AuthenticationStateProvider authenticationStateProvider)
    : AccountBaseService(httpClientFactory, navigationManager), ILoginService
{
    public async Task<bool> LoginAsync(LoginModel loginModel)
    {
        var response = await HttpClient.PostAsJsonAsync("api/Account/Login", loginModel);
        var content = await response.Content.ReadFromJsonAsync<ResponseObject<UserLoggedInModel>>();

        if (content is null || !content.IsSuccess)
        {
            //TODO: Log errors? Handle errors ?
            return false;
        }

        var userLoggedIn = content.Result;
        await protectedLocalStorage.SetAsync(TokenStorageKey, userLoggedIn);
        ((CustomAuthenticationProvider)authenticationStateProvider).AuthenticateUser(userLoggedIn.Token);

        return true;
    }

}
