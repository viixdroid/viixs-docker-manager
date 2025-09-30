using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ViixsDockerManager.Shared.AspNet.Startup;
using ViixsDockerManager.Users.Accounts.Helpers;

namespace ViixsDockerManager.Users.Accounts.Startup;

public sealed class UserAccountsConfigureAppComponent() : ConfigureAppComponent
{
    protected override void ConfigureApplication(WebApplication webApplication)
    {
        using var scope = webApplication.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        serviceProvider.SeedApplicationRoles().GetAwaiter();
    }
}
