using DockerManager.DockerControl.Models;
using DockerManager.DockerController.Views.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace DockerManager.DockerController.Views.Frontend;

public partial class Containers(IContainerService containerService) : ComponentBase
{
    private IEnumerable<ContainerSummary> _containerSummaries = [];

    protected override async Task OnInitializedAsync()
    {
        _containerSummaries = await containerService.GetContainers();
    }
}
