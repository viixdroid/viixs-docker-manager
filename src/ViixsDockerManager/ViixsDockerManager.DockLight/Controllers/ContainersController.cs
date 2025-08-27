using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViixsDockerManager.DockLight.Models;
using ViixsDockerManager.DockLight.Services.Interfaces;
using ViixsDockerManager.Shared.Controllers;
using ViixsDockerManager.Shared.Exceptions;
using ViixsDockerManager.Shared.Models;

namespace ViixsDockerManager.DockLight.Controllers;

public class ContainersController(IDockerContainersService dockerContainersService)
    : ViixsBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetContainers()
    {
        var result = await dockerContainersService.GetContainerListAsync();
        return Ok(ResponseObject<IEnumerable<ContainerSummary>>.Success(result));
    }
}
