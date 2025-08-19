using Microsoft.AspNetCore.Components;

using static DockerManager.Shared.Constants.ApplicationConstants;

namespace DockerManager.Auth.Services.Account;

public abstract class AccountBaseService
{
    protected HttpClient HttpClient { get; }

    protected AccountBaseService(IHttpClientFactory httpClientFactory, NavigationManager navigationManager)
    {
        HttpClient = httpClientFactory.CreateClient(BackendApiHttpClientName);
        HttpClient.BaseAddress = new Uri(navigationManager.BaseUri);
    }
}
