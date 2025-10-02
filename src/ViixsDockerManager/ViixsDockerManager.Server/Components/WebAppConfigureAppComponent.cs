using Serilog;
using ViixsDockerManager.Shared.AspNet.Extensions;
using ViixsDockerManager.Shared.AspNet.Startup;

namespace ViixsDockerManager.Server.Components;

internal sealed class WebAppConfigureAppComponent : ConfigureAppComponent
{
    protected override void ConfigureApplication(WebApplication webApplication)
    {
        webApplication.UseExceptionHandlerService();

        webApplication.UseSerilogRequestLogging();

        webApplication.MapDefaultEndpoints();

        webApplication.UseDefaultFiles();
        webApplication.MapStaticAssets();
        if (webApplication.Environment.IsDevelopment())
        {
            webApplication.MapOpenApi();
        }
        webApplication.UseHttpsRedirection();

        webApplication.UseAuthorization();

        webApplication.MapFallbackToFile("/index.html");
    }
}
