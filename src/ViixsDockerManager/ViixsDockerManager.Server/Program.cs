using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ViixsDockerManager.Shared.AspNet.Extensions;
using ViixsDockerManager.Shared.AspNet.Filters;
using ViixsDockerManager.DockLight.Environments.Extensions;
using ViixsDockerManager.DockLight.Extensions;
using ViixsDockerManager.DockLight.Shared.Extensions;
using ViixsDockerManager.Mediator.Extensions;
using ViixsDockerManager.Server.Builders;
using ViixsDockerManager.Shared.Database.Sqlite.Extensions;
using ViixsDockerManager.Shared.Extensions;
using ViixsDockerManager.Users.Accounts.Extensiosn;

var webApp = WebAppStarter.Instance
    .CreateWebApplicationBuilder(args, builder =>
{
    builder.Configuration.AddEnvironmentVariables();
    builder.AddServiceDefaults();
})
    .ConfigureWebAppBuilder()
    .BuildWebApp()
    .RunMigrations()
    .ConfigureApplication();

await webApp.RunApp(default);

//var programBuilder = ProgramBuilder.Create(args);
//await programBuilder
//    .ConfigureBuilder(builder =>
//    {
//        builder.Configuration.AddEnvironmentVariables();
//    })
//    .EnrichHost()
//    //.ConfigureServices()
//    .Build()
//    //.ConfigureApplication()
//    .Start();

//var builder = WebApplication.CreateBuilder(args);

//builder.Configuration.AddEnvironmentVariables();

//var logFileLocation = builder.Configuration.GetSection("ViixsDockerManager").GetValue<string>("logfilelocation") ?? throw ViixsDockerManager.Shared.Exceptions.ViixsDockerManagerException.InvalidOperation("No log file locations");
//const string logTemplate = "[{Timestamp:HH:mm:ss}] [{Level:u4}] [{SourceContext}] {Message:j}{NewLine}{Exception}";

//builder.Host.UseSerilog((context, services, configuration) => configuration
//    .ReadFrom.Configuration(context.Configuration)
//    .ReadFrom.Services(services)
//    .Enrich.FromLogContext()
//    .WriteTo.Console(outputTemplate: logTemplate)
//    .WriteTo.File(logFileLocation!, rollingInterval: RollingInterval.Day, outputTemplate: logTemplate)
//    .WriteTo.OpenTelemetry(
//        // The endpoint below needs to be accessible from your service.
//        // Aspire automatically sets up environment variables for the OTLP endpoint.
//        // You might use Configuration.GetConnectionString("otlp") or a specific env var.
//        endpoint: context.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"] ?? "http://localhost:4317",
//        protocol: Serilog.Sinks.OpenTelemetry.OtlpProtocol.Grpc,
//        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
//    )
//);

//builder.AddServiceDefaults();

//// Add services to the container.
//builder.Services.AddMediatorServices();
//builder.Services.AddDockLightContainerServices();
//builder.Services.AddDockLightEnvironmentServices(builder.Configuration);
//builder.Services.AddDockLightSharedServices();
//builder.Services.AddUserAccountsService(builder.Configuration);
//builder.Services.AddUserAccountCommandHandlers();
//builder.Services.AddExceptionHandlerService();

//builder.Services.AddSignalR()
//    .AddJsonProtocol(options =>
//{
//    options.PayloadSerializerOptions.Converters.Add(new JsonStringEnumConverter(namingPolicy: JsonNamingPolicy.SnakeCaseLower));
//});
//builder.Services.AddControllers(options =>
//{
//    options.Filters.Add<WrapControllerResultFilter>();
//});
//// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
//builder.Services.ConfigureHttpJsonOptions(options =>
//{
//    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter(namingPolicy: JsonNamingPolicy.SnakeCaseLower));
//});
//builder.Services.AddMemoryCache();

//var app = builder.Build();

//app.UseExceptionHandlerService();

//app.AddDocklightEnvironmentMigrations();
//app.AddUserAccountsMigrations();

//app.RunMigrations();

//app.UseSerilogRequestLogging();

//await app.SeedRolesAsync();

//var webSocketGroup = app.MapGroup("ws");
//webSocketGroup.MapDockLightSignalRHubs();
//webSocketGroup.MapUserAccountsSignalRHubs();

//var builderWithAppliedEndpointFilter = app.ApplyEndpointFilter();
//var apiGroup = builderWithAppliedEndpointFilter.MapGroup("api");

//var environmentRoot = apiGroup.MapDocklightEnvironmentRoutes();
//environmentRoot.MapDockLightEnvironmentSetupRoutes();
//environmentRoot.MapDocklightRoutes();

//apiGroup.MapUserAccountRoutes();

//app.MapDefaultEndpoints();

//app.UseDefaultFiles();
//app.MapStaticAssets();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.MapFallbackToFile("/index.html");

//app.Run();
