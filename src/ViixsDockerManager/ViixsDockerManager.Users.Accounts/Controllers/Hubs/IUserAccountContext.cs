namespace ViixsDockerManager.Users.Accounts.Controllers.Hubs;

public interface IUserAccountContext
{
    Task OnAccountCreated(string emailAddress, bool accountCreatedSuccesfully);
}
