using Serilog;
using ViixsDockerManager.DockLight.Extensions;
using ViixsDockerManager.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
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
builder.Services.AddDockLightServices();
builder.Services.AddExceptionHandlerService();


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var app = builder.Build();

app.UseExceptionHandlerService();

app.UseSerilogRequestLogging();

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
