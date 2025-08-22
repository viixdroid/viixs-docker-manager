using System.Net.Http.Json;
using DockerManager.DockerControl.Models;
using DockerManager.DockerController.Views.Services.Interfaces;
using DockerManager.Shared.Models.Responses;
using Microsoft.AspNetCore.Components;
using static DockerManager.Shared.Constants.ApplicationConstants;

namespace DockerManager.DockerController.Views.Services;

public class ContainerService : IContainerService
{
    private readonly HttpClient _httpClient;

    public ContainerService(IHttpClientFactory httpClientFactory, NavigationManager navigationManager)
    {
        _httpClient = httpClientFactory.CreateClient(BackendApiHttpClientName);
        _httpClient.BaseAddress = new Uri(navigationManager.BaseUri);
    }

    public async Task<IEnumerable<ContainerSummary>> GetContainers()
    {
        var response = await _httpClient.GetAsync("api/Docker/Containers");
        try
        {
            var content = await response.Content.ReadFromJsonAsync<ResponseObject<IEnumerable<ContainerSummary>>>();

            if (content is null || !content.IsSuccess)
            {
                return [];
            }

            return content.Result ?? [];
        }
        catch (Exception ex)
        {
            // Log the exception or handle it as needed
            Console.WriteLine($"Error fetching containers: {ex.Message}");
            return [];
        }
    }
}
