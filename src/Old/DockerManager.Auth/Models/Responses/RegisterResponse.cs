using DockerManager.Shared.Models.Responses;

namespace DockerManager.Auth.Models.Responses;

public record RegisterResponse(string? Message, string? RedirectUrl, bool IsSuccess) : ResponseObject(IsSuccess)
{
    public static RegisterResponse Success(string redirectUrl) =>
        new RegisterResponse("Registration successful. Please log in.", redirectUrl, true);
}
