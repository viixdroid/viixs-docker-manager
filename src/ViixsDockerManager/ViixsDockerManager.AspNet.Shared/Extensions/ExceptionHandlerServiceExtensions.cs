using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ViixDockerManager.AspNet.Shared.Handlers;

namespace ViixDockerManager.AspNet.Shared.Extensions;

public static class ExceptionHandlerServiceExtensions
{
    public static IServiceCollection AddExceptionHandlerService(this IServiceCollection services)
    {
        services.AddProblemDetails();
        services.AddExceptionHandler<ViixsDockerManagerExceptionHandler>();
        return services;
    }

    public static IApplicationBuilder UseExceptionHandlerService(this IApplicationBuilder app)
    {
        app.UseExceptionHandler();
        return app;
    }
}
