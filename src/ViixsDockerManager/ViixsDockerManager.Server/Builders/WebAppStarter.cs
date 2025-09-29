using System.Reflection;
using Serilog;
using ViixsDockerManager.Server.Helpers;
using ViixsDockerManager.Server.Startup;
using ViixsDockerManager.Server.Startup.Factories;
using ViixsDockerManager.Shared.Database.Migrations;
using ViixsDockerManager.Shared.Helpers;

namespace ViixsDockerManager.Server.Builders;

public class WebAppStarter : ICreateWebApplicationBuilder, IWebAppStarter, IConfigureWebAppBuilder, IStartWebApp
{
    private static readonly Lazy<ICreateWebApplicationBuilder> _webAppStarterInstance = new Lazy<ICreateWebApplicationBuilder>(() => new WebAppStarter());

    private WebApplication? _webApplication;
    private WebApplicationBuilder? _webApplicationBuilder;

    private IEnumerable<IStartupFeature>? _startupFeatures;

    private Serilog.ILogger? _logger;

    private WebAppStarter() { }

    public static ICreateWebApplicationBuilder Instance => _webAppStarterInstance.Value;
    
    public IWebAppStarter CreateWebApplicationBuilder(string[] applicationArgs, Action<WebApplicationBuilder> initializationAction)
    {
        _webApplicationBuilder = WebApplication.CreateBuilder(applicationArgs);
        initializationAction(_webApplicationBuilder);
        var configuration = _webApplicationBuilder.Configuration;
        Log.Logger = new LoggerConfiguration()
            .GetLoggerConfiguration(configuration)
            .CreateBootstrapLogger();
        _logger = Log.Logger.ForContext<WebAppStarter>();
        _logger.Information("Starting ViixsDockerManager {Version}", Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion[..14]);
        _startupFeatures = new StartupFeatureFactory(configuration).GetStartupFeatures();
        return this;
    }

    public IConfigureWebAppBuilder ConfigureWebAppBuilder()
    {
        _logger?.Information("Configuring Web application builder");
        _startupFeatures = Guard.ValueIsNotNull(_startupFeatures, nameof(_startupFeatures));
        _webApplicationBuilder = Guard.ValueIsNotNull(_webApplicationBuilder, nameof(_webApplicationBuilder));
        foreach (var feature in _startupFeatures)
        {
            feature.ConfigureBuilder(_webApplicationBuilder);
        }
        return this;
    }

    public IStartWebApp BuildWebApp()
    {
        _logger?.Information("Creating app");
        _startupFeatures = Guard.ValueIsNotNull(_startupFeatures, nameof(_startupFeatures));
        _webApplicationBuilder = Guard.ValueIsNotNull(_webApplicationBuilder, nameof(_webApplicationBuilder));
        _webApplication = _webApplicationBuilder.Build();
        return this;
    }

    public IStartWebApp RunMigrations()
    {
        _logger?.Information("Running migrations");
        _startupFeatures = Guard.ValueIsNotNull(_startupFeatures, nameof(_startupFeatures));
        _webApplication = Guard.ValueIsNotNull(_webApplication, nameof(_webApplication));
        var migrationRunner = new MigrationRunner();
        foreach (var feature in _startupFeatures)
        {
            feature.GetMigrations(migrationRunner);
        }
        migrationRunner.RunMigrations(_webApplication);
        return this;
    }

    public IStartWebApp ConfigureApplication()
    {
        _logger?.Information("Configuring app and routes");
        _startupFeatures = Guard.ValueIsNotNull(_startupFeatures, nameof(_startupFeatures));
        _webApplication = Guard.ValueIsNotNull(_webApplication, nameof(_webApplication));
        foreach (var feature in _startupFeatures)
        {
            feature.ConfigureApplication(_webApplication);
        }
        return this;
    }

    public Task RunApp(CancellationToken cancellationToken)
    {
        _webApplication = Guard.ValueIsNotNull(_webApplication, nameof(_webApplication));
        _logger?.Information("Application Initialization Done.");
        _logger?.Information("Starting app");
        return _webApplication.RunAsync(cancellationToken);
    }    
}
