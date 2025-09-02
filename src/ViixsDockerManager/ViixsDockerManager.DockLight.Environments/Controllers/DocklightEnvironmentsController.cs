using Microsoft.AspNetCore.Mvc;
using ViixDockerManager.AspNet.Shared.Controllers;
using ViixsDockerManager.DockLight.Shared.Entities;
using ViixsDockerManager.Shared.Database.Repositories;

namespace ViixsDockerManager.DockLight.Environments.Controllers;

public class DocklightEnvironmentsController(IReadRepository<DocklightEnvironment> readRepository)
    : ViixsBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAllDocklightEnvironments()
    {
        var environments = await readRepository.GetAllAsync();
        return Ok(environments);
    }
}
