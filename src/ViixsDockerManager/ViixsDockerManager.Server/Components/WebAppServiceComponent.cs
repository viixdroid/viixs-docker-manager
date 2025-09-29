using System.Text.Json;
using System.Text.Json.Serialization;
using ViixsDockerManager.Shared.AspNet.Filters;
using ViixsDockerManager.Shared.AspNet.Startup;

namespace ViixsDockerManager.Server.Components;

internal class WebAppServiceComponent : ServiceComponent
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddSignalR()
            .AddJsonProtocol(options =>
        {
            options.PayloadSerializerOptions.Converters.Add(new JsonStringEnumConverter(namingPolicy: JsonNamingPolicy.SnakeCaseLower));
        });
        services.AddControllers(options =>
        {
            options.Filters.Add<WrapControllerResultFilter>(); //Old style controllers
        });
        services.AddOpenApi();
        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter(namingPolicy: JsonNamingPolicy.SnakeCaseLower));
        });
        services.AddMemoryCache();
    }
}
