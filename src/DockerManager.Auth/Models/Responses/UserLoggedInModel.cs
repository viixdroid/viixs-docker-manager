namespace DockerManager.Auth.Models.Responses;

public record UserLoggedInModel(string UserId, string UserName, string Email, string Token);
