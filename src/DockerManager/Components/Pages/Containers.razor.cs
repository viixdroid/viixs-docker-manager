using DockerManager.Shared.Models.Docker;
using DockerManager.Shared.Services.Docker.Interfaces;
using Microsoft.AspNetCore.Components;

namespace DockerManager.Components.Pages;

public partial class Containers(IDockerService dockerService) : ComponentBase
{
    private IEnumerable<ContainerSummary> _containerSummaries = [];

    protected override async Task OnInitializedAsync()
    {
        _containerSummaries = await dockerService.GetContainerListAsync();
    }
}