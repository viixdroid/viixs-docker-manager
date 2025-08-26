using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViixsDockerManager.DockLight.Services.Interfaces;
using ViixsDockerManager.Shared.Controllers;
using ViixsDockerManager.Shared.Exceptions;

namespace ViixsDockerManager.DockLight.Controllers;

public class ContainersController(IDockerContainersService dockerContainersService)
    : ViixsBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetContainers()
    {
        try
        {
            var result = await dockerContainersService.GetContainerListAsync();
            return Ok(result);
        }
        catch (ViixsDockerManagerException)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                System.Text.Json.JsonSerializer.Serialize("We could not return the expected object."));
        }
    }
}
