using Microsoft.AspNetCore.Mvc;

namespace DockerManager.Auth.Controllers.Account;

[ApiController]
[Route("api/Account/[controller]")]
public abstract class AccountControllerBase : ControllerBase
{
}