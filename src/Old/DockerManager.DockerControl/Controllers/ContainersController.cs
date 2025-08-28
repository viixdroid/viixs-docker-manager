using DockerManager.DockerControl.Models;
using DockerManager.DockerControl.Services.Interfaces;
using DockerManager.Shared.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DockerManager.DockerControl.Controllers;

[ApiController]
[Route("api/Docker/[controller]")]
// [Authorize]
public class ContainersController(IDockerService dockerService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetContainersAsync()
    {
        var result = await dockerService.GetContainerListAsync();
        return Ok(ResponseObject<IEnumerable<ContainerSummary>>.Success(result));
    }
}
