using System.Net.Http.Json;
using DockerManager.Auth.Models.Pages;
using DockerManager.Auth.Models.Responses;
using DockerManager.Auth.Views.Services.Interfaces.Account;
using Microsoft.AspNetCore.Components;

namespace DockerManager.Auth.Views.Services.Account;

internal class RegisterService(IHttpClientFactory httpClientFactory, NavigationManager navigationManager)
    : AccountBaseService(httpClientFactory, navigationManager), IRegisterService
{
    /// <inheritdoc />
    public async Task<(bool, string)> RegisterAsync(RegisterModel model)
    {
        var result = await HttpClient.PostAsJsonAsync("api/Account/Register", model);

        if (result.IsSuccessStatusCode)
        {
            var response = await result.Content.ReadFromJsonAsync<RegisterResponse>();

            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (response?.RedirectUrl == null)
            {
                return (false, "Registration successful, but no redirect URL provided.");
            }

            return (true, response.RedirectUrl);
        }

        var errorMessage = await result.Content.ReadAsStringAsync();
        return (false, errorMessage);
    }
}
