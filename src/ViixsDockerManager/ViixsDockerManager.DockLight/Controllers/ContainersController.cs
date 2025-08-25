using Microsoft.AspNetCore.Mvc;
using ViixsDockerManager.DockLight.Services.Interfaces;
using ViixsDockerManager.Shared.Controllers;

namespace ViixsDockerManager.DockLight.Controllers;

public class ContainersController(IDockerContainersService dockerContainersService)
    : ViixsBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetContainers()
    {
        var result = await dockerContainersService.GetContainerListAsync();
        return Ok(result);
    }
}
