using Microsoft.AspNetCore.Components;

namespace DockerManager.Components.Pages;

public partial class ContainerDetails : ComponentBase
{
    [Parameter] 
    public string ContainerName { get; set; }
}