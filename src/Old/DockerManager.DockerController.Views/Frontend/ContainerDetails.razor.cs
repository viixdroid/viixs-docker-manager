using Microsoft.AspNetCore.Components;

namespace DockerManager.DockerController.Views.Frontend;

public partial class ContainerDetails : ComponentBase
{
    [Parameter]
    public string? ContainerName { get; set; }
}
