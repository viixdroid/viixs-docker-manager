using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ViixsDockerManager.Mediator.Extensions;
using ViixsDockerManager.Shared.AspNet.Startup;
using ViixsDockerManager.Shared.Database.Models;
using ViixsDockerManager.Shared.Database.Sqlite.Extensions;
using ViixsDockerManager.Shared.Extensions;
using ViixsDockerManager.Users.Accounts.Commands;
using ViixsDockerManager.Users.Accounts.DbContexts;
using ViixsDockerManager.Users.Accounts.Handlers;
using ViixsDockerManager.Users.Accounts.Models;
using ViixsDockerManager.Users.Accounts.Services;
using ViixsDockerManager.Users.Accounts.Services.Interfaces;

namespace ViixsDockerManager.Users.Accounts.Startup;

public sealed class UserAccountsServiceComponent(IConfiguration configuration) : ServiceComponent
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddDecoration<ISendUserAccountNotifications, SendUserAccountNotifications>();
        services.AddDbContextPool<UserAccountDbContext>(configuration); // for identity

        services.AddIdentityCore<ViixsDockerManagerUser>(options => options.User.RequireUniqueEmail = true)
            .AddRoles<ViixsDockerManagerRole>()
            .AddEntityFrameworkStores<UserAccountDbContext>();

        services.AddWriteDatabaseServices<UserAccountDbContext>(configuration, migrationsHistory: new MigrationsHistory("UserIdentity"));
    }

    protected override void ConfigureCommandHandlers(IServiceCollection services)
    {
        services.RegisterCommandHandler<CreateUserAccountHandler, CreateUserAccountCommand>();
    }
}
