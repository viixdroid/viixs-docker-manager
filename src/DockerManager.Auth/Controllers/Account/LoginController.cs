using DockerManager.Auth.Models;
using DockerManager.Auth.Models.Pages;
using DockerManager.Auth.Models.Responses;
using DockerManager.Auth.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DockerManager.Auth.Controllers.Account;

public class LoginController(
    UserManager<DockerManagerUser> userManager,
    SignInManager<DockerManagerUser> signInManager,
    IJwtUtilService jwtUtilService,
    IDockerManagerUserService dockerManagerUserService)
    : AccountControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await userManager.FindByNameAsync(model.Email);
        if (user == null)
        {
            return NotFound(ResponseObject.Failure("User not found."));
        }

        var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);

        //TODO: Handle lockout, two-factor authentication, etc.

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status401Unauthorized,
                ResponseObject.Failure("Invalid login attempt. Please check your email and password."));
        }

        var dockerManagerUser = await dockerManagerUserService.GetUserByEmailWithRolesTask(user.UserName!);
        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status401Unauthorized,
                ResponseObject.Failure("Invalid login attempt. Please check your email and password."));
        }

        var token = jwtUtilService.GenerateJwtToken(dockerManagerUser!);

        if (string.IsNullOrEmpty(token))
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                ResponseObject.Failure("Failed to generate JWT token."));
        }

        return Ok(ResponseObject<UserLoggedInModel>.Success(new UserLoggedInModel(user.Id, user.UserName!, user.Email!,
            token)));
    }
}