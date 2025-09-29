using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ViixsDockerManager.Mediator.Extensions;
using ViixsDockerManager.Shared.Database.Models;
using ViixsDockerManager.Shared.Database.Sqlite.Extensions;
using ViixsDockerManager.Shared.Extensions;
using ViixsDockerManager.Users.Accounts.Commands;
using ViixsDockerManager.Users.Accounts.Controllers;
using ViixsDockerManager.Users.Accounts.Controllers.Hubs;
using ViixsDockerManager.Users.Accounts.DbContexts;
using ViixsDockerManager.Users.Accounts.Handlers;
using ViixsDockerManager.Users.Accounts.Helpers;
using ViixsDockerManager.Users.Accounts.Models;
using ViixsDockerManager.Users.Accounts.Services;
using ViixsDockerManager.Users.Accounts.Services.Interfaces;

namespace ViixsDockerManager.Users.Accounts.Extensiosn;

public static class UserAccountsServiceExtensions
{
    private const string InitialAdministratorRole = "Administrator";
    public static IServiceCollection AddUserAccountsService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDecoration<ISendUserAccountNotifications, SendUserAccountNotifications>();
        services.AddDbContextPool<UserAccountDbContext>(configuration); // for identity

        services.AddIdentityCore<ViixsDockerManagerUser>(options => options.User.RequireUniqueEmail = true)
            .AddRoles<ViixsDockerManagerRole>()
            .AddEntityFrameworkStores<UserAccountDbContext>();

        //services.AddReadDatabaseServices<UserAccountDbContext>(configuration);
        services.AddWriteDatabaseServices<UserAccountDbContext>(configuration, migrationsHistory: new MigrationsHistory("UserIdentity"));
        //    options =>
        //{
        //    options.UseSeeding((context, storeOperationPerformed) =>
        //    {
        //        var roleManager = context.GetService<RoleManager<IdentityRole>>();
        //        var roleExists = roleManager.RoleExistsAsync(InitialAdministratorRole).GetAwaiter().GetResult();
        //        if (roleExists)
        //        {
        //            return;
        //        }
        //        var isCreated = roleManager.CreateAsync(new ViixsDockerManagerRole(InitialAdministratorRole)).GetAwaiter().GetResult();
        //        if (!isCreated.Succeeded)
        //        {
        //            var logger = context?.GetService<ILoggerFactory>()?.CreateLogger(nameof(UserAccountsServiceExtensions));
        //            logger?.LogWarning("Could not create the inital role for {InitialAdministratorRole}", InitialAdministratorRole);
        //        }
        //    });
        //},

        return services;
    }

    public static IServiceCollection AddQueryHandlers(this IServiceCollection services)
    {
        // services.RegisterQueryHandler<,>();
        return services;
    }

    public static IServiceCollection AddUserAccountCommandHandlers(this IServiceCollection services)
    {
        // services.RegisterCommandHandler<,>();
        services.RegisterCommandHandler<CreateUserAccountHandler, CreateUserAccountCommand>();
        return services;
    }

    public static RouteGroupBuilder MapUserAccountRoutes(this IEndpointRouteBuilder endPointRouteBuilder)
    {
        var group = endPointRouteBuilder.MapGroup("/users");
        group.MapUserAccountRouteActions();
        return group;
    }

    public static RouteGroupBuilder MapUserAccountsSignalRHubs(this IEndpointRouteBuilder endPointRouteBuilder)
    {
        var group = endPointRouteBuilder.MapGroup("/users");
        group.MapHub<UserAccountHub>("/account");
        return group;
    }

    //public static void AddUserAccountsMigrations(this IHost serviceHost)
    //    => serviceHost.AddContextForMigrationRunning<UserAccountDbContext>();

    public static async Task SeedRolesAsync(this IHost serviceHost)
    {
        using var scope = serviceHost.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        await serviceProvider.SeedApplicationRoles();
    }
}
