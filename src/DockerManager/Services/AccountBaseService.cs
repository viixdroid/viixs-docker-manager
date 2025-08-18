using Microsoft.AspNetCore.Components;
using static DockerManager.Constants.ApplicationConstants;

namespace DockerManager.Services;

public abstract class AccountBaseService
{
    protected HttpClient HttpClient { get; }

    protected AccountBaseService(IHttpClientFactory httpClientFactory, NavigationManager navigationManager)
    {
        HttpClient = httpClientFactory.CreateClient(BackendApiHttpClientName);
        HttpClient.BaseAddress = new Uri(navigationManager.BaseUri);
    }
}