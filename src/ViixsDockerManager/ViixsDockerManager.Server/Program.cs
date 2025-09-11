using Microsoft.EntityFrameworkCore;
using Serilog;
using ViixDockerManager.AspNet.Shared.Extensions;
using ViixDockerManager.AspNet.Shared.Filters;
using ViixsDockerManager.DockLight.Environments.Extensions;
using ViixsDockerManager.DockLight.Extensions;
using ViixsDockerManager.DockLight.Shared.Extensions;
using ViixsDockerManager.Mediator.Extensions;
using ViixsDockerManager.Shared.Database.Sqlite.Extensions;
using ViixsDockerManager.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

var logFileLocation = builder.Configuration.GetSection("ViixsDockerManager").GetValue<string>("logfilelocation") ?? throw ViixsDockerManager.Shared.Exceptions.ViixsDockerManagerException.InvalidOperation("No log file locations");
const string logTemplate = "[{Timestamp:HH:mm:ss}] [{Level:u4}] [{SourceContext}] {Message:j}{NewLine}{Exception}";


builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console(outputTemplate: logTemplate)
    .WriteTo.File(logFileLocation!, rollingInterval: RollingInterval.Day, outputTemplate: logTemplate)
    .WriteTo.OpenTelemetry(
        // The endpoint below needs to be accessible from your service.
        // Aspire automatically sets up environment variables for the OTLP endpoint.
        // You might use Configuration.GetConnectionString("otlp") or a specific env var.
        endpoint: context.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"] ?? "http://localhost:4317",
        protocol: Serilog.Sinks.OpenTelemetry.OtlpProtocol.Grpc,
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
    )
);

builder.AddServiceDefaults();


// Add services to the container.
builder.Services.AddMediatorServices();
builder.Services.AddDockLightServices();
builder.Services.AddDockLightEnvironmentServices(builder.Configuration);
builder.Services.AddDockLightSharedServices();
builder.Services.AddExceptionHandlerService();


builder.Services.AddControllers(options =>
{
    options.Filters.Add<WrapControllerResultFilter>();
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var app = builder.Build();

app.UseExceptionHandlerService();

await app.RunDocklightEnvironmentMigrations();

app.UseSerilogRequestLogging();

var builderWithAppliedEndpointFilter = app.ApplyEndpointFilter();
var environmentRoot = builderWithAppliedEndpointFilter.MapDocklightEnvironmentRoutes();
environmentRoot.MapDockLightEnvironmentSetupRoutes();
environmentRoot.MapDocklightRoutes();

app.MapDefaultEndpoints();

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
