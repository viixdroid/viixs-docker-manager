using Microsoft.Extensions.DependencyInjection;

namespace DockerManager.Auth.Views.Extensions;

public static class DockerManagerAuthenticationViewServicesExtensions
{
    public static IServiceCollection AddDockerManagerAuthenticationViewServices(this IServiceCollection services)
    {
        // Register authentication view services here
        services.AddScoped<Services.Interfaces.Account.ILoginService, Services.Account.LoginService>();
        services.AddScoped<Services.Interfaces.Account.IRegisterService, Services.Account.RegisterService>();
        // services.AddScoped<Services.Interfaces.Account.IAccountService, Services.Account.AccountService>();

        return services;
    }
}
