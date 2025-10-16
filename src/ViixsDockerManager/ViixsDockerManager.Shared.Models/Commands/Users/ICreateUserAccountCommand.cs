namespace ViixsDockerManager.Shared.Models.Commands.Users;

public interface ICreateUserAccountCommand
{
    string EmailAddress { get; }
    string Password { get; }
    string Role { get; }
}
