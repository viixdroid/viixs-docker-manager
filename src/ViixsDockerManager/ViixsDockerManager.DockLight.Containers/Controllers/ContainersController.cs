using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViixDockerManager.AspNet.Shared.Controllers;
using ViixsDockerManager.DockLight.Services.Interfaces;
using ViixsDockerManager.Shared.Exceptions;
using ViixsDockerManager.Shared.Models;

namespace ViixsDockerManager.DockLight.Controllers;

public class ContainersController(IDockerContainersService dockerContainersService)
    : ViixsBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetContainers()
    {
        var result = await dockerContainersService.GetContainerListAsync(Guid.Empty);
        return Ok(result);
    }
}
