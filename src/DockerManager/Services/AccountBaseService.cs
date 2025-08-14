using Microsoft.AspNetCore.Components;

namespace DockerManager.Services;

public abstract class AccountBaseService
{
    protected HttpClient HttpClient { get; }

    protected AccountBaseService(HttpClient httpClient, NavigationManager navigationManager)
    {
        HttpClient = httpClient;
        HttpClient.BaseAddress = new Uri(navigationManager.BaseUri);
    }
}