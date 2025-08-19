using System.Text;
using DockerManager.Auth.DbContext;
using DockerManager.Auth.Models;
using DockerManager.Auth.Models.Settings;
using DockerManager.Auth.Providers;
using DockerManager.Auth.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace DockerManager.Auth.Extensions;

public static class AuthenticationServiceExtensions
{
    public static IServiceCollection AddAuthenticationService(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                               throw new InvalidOperationException(
                                   "Connection string 'DefaultConnection' not found in configuration.");

        services.AddScoped<IDockerManagerUserService, DockerManagerUserService>();
        services.AddScoped<IJwtUtilService, JwtUtilService>();
        services.AddCascadingAuthenticationState();

        services.AddDbContext<AuthenticationDbContext>(options => options.UseSqlite(connectionString));
        services.AddIdentityCore<DockerManagerUser>(options => options.User.RequireUniqueEmail = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AuthenticationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        services.AddJwtAuthentication(configuration);

//TODO: move to a more appropriate place
        services.AddScoped<AuthenticationStateProvider, CustomAuthenticationProvider>();

        return services;
    }

    private static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();
        if (jwtOptions is null)
        {
            throw new InvalidOperationException("JWT options are not configured in the application settings.");
        }

        if (jwtOptions.Secret.Length < 16)
        {
            throw new InvalidOperationException("JWT secret must be at least 16 characters long.");
        }


        var key = Encoding.ASCII.GetBytes(jwtOptions.Secret);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(jwtBearerOptions =>
        {
            jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtOptions.Audience
            };
        });

        services.AddAuthorizationBuilder()
            .AddPolicy("Default", new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build())
            .AddPolicy("Administrator", new AuthorizationPolicyBuilder()
                .RequireRole("Administrator")
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build());

        return services;
    }

    public static async Task RunMigrations(this IHost serviceHost)
    {
        using var scope = serviceHost.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AuthenticationDbContext>();
        await dbContext.Database.MigrateAsync();
    }
}
