using DockerManager.Auth.Models;
using DockerManager.Auth.Models.Pages;
using DockerManager.Auth.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DockerManager.Auth.Controllers.Account;

public class RegisterController(
    IUserStore<DockerManagerUser> userStore,
    UserManager<DockerManagerUser> userManager,
    RoleManager<IdentityRole> roleManager)
    : AccountControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterModel? model, CancellationToken cancellationToken)
    {
        if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
        {
            return BadRequest(ResponseObject.Failure("Invalid registration data."));
        }

        var user = new DockerManagerUser();
        await userStore.SetUserNameAsync(user, model.Email, cancellationToken);
        await userManager.SetEmailAsync(user, model.Email); //Prevents "email is invalid" error
        var result = await userManager.CreateAsync(user, model.Password);

        // ReSharper disable once ConvertIfStatementToReturnStatement
        if (!result.Succeeded)
        {
            return MapIdentityErrors(result);
        }

        const string administratorRole = "Administrator";
        await AddRoleIfNotExistsAsync(administratorRole);
        await userManager.AddToRoleAsync(user, administratorRole);

        // Simulate successful registration
        return Created("/Account/Login", RegisterResponse.Success("/Account/Login"));
    }

    //TODO: move to more better place than here.
    private async Task AddRoleIfNotExistsAsync(string roleName)
    {
        var roleExists = await roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }


    private IActionResult MapIdentityErrors(IdentityResult result)
    {
        if (result.Errors.Any(e =>
                e.Code is "DuplicateUserName" or "DuplicateEmail"))
        {
            return Conflict(ResponseObject.Failure("A user with this email already exists."));
        }

        if (result.Errors.Any(e =>
                e.Code.StartsWith("Password") || e.Code.StartsWith("Invalid")))
        {
            return BadRequest(ResponseObject.Failure(result.Errors.Select(e => e.Description)));
        }

        return StatusCode(StatusCodes.Status500InternalServerError,
            ResponseObject.Failure(result.Errors.Select(e => e.Description)));
    }
}
