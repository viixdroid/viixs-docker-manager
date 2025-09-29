using System.Reflection;
using Serilog;
using ViixsDockerManager.Server.Builders.EventHandlers;
using ViixsDockerManager.Server.Builders.EventHandlers.EventArguments;
using ViixsDockerManager.Server.Builders.Interfaces;
using ViixsDockerManager.Server.Helpers;
using ViixsDockerManager.Server.Startup.Factories;
using ViixsDockerManager.Server.Startup.Interfaces;
using ViixsDockerManager.Shared.Database.Migrations;
using ViixsDockerManager.Shared.Helpers;

namespace ViixsDockerManager.Server.Builders;

public class WebAppStarter : IWebAppStarterConfigurator, IWebAppStarter, IConfigureWebAppBuilder, IStartWebApp, IWebAppStarterEvents
{
    private static readonly Lazy<IWebAppStarterConfigurator> _webAppStarterInstance = new Lazy<IWebAppStarterConfigurator>(() => new WebAppStarter());

    private WebApplication? _webApplication;
    private WebApplicationBuilder? _webApplicationBuilder;

    //private IEnumerable<IStartupFeature>? _startupFeatures;
    private IStartupFeatureFactory? _startupFeatureFactory;

    private WebAppStarter()
    {
    }
    public static IWebAppStarterConfigurator Instance => _webAppStarterInstance.Value;

    #region Eventhandlers
    public event EventHandler<InitializingEventArgs>? OnInitializing;
    public event EventHandler<InitializedEventArgs>? OnInitialized;
    public event EventHandler<ConfiguringServicesEventArgs>? OnConfiguringServices;
    public event EventHandler<ServicesConfiguredEventArgs>? OnServicesConfigured;
    public event EventHandler<ConfiguringApplicationEventArgs>? OnConfiguringApplication;
    public event EventHandler<ApplicationConfiguredEventArgs>? OnApplicationConfigured;
    public event EventHandler<ApplicationStartedEventArgs>? OnApplicationStarted;
    public event EventHandler<StartingApplicationEventArgs>? OnApplicationStarting;

    private void NotifyOnInitializing(InitializingEventArgs initializingEventArgs)
    {
        initializingEventArgs = Guard.ValueIsNotNull(initializingEventArgs, nameof(initializingEventArgs));
        OnInitializing?.Invoke(this, initializingEventArgs);
    }

    private void NotifyOnInitialized(InitializedEventArgs initializedEventArgs)
    {
        initializedEventArgs = Guard.ValueIsNotNull(initializedEventArgs, nameof(initializedEventArgs));
        OnInitialized?.Invoke(this, initializedEventArgs);
    }

    private void NotifyOnConfiguringServices(ConfiguringServicesEventArgs configuringServicesEventArgs)
    {
        configuringServicesEventArgs = Guard.ValueIsNotNull(configuringServicesEventArgs, nameof(configuringServicesEventArgs));
        OnConfiguringServices?.Invoke(this, configuringServicesEventArgs);
    }

    private void NotifyOnServicesConfigured(ServicesConfiguredEventArgs servicesConfiguredEventArgs)
    {
        servicesConfiguredEventArgs = Guard.ValueIsNotNull(servicesConfiguredEventArgs, nameof(servicesConfiguredEventArgs));
        OnServicesConfigured?.Invoke(this, servicesConfiguredEventArgs);
    }

    private void NotifyOnConfiguringApplication(ConfiguringApplicationEventArgs configuringApplicationEventArgs)
    {
        configuringApplicationEventArgs = Guard.ValueIsNotNull(configuringApplicationEventArgs, nameof(configuringApplicationEventArgs));
        OnConfiguringApplication?.Invoke(this, configuringApplicationEventArgs);
    }

    private void NotifyOnApplicationConfigured(ApplicationConfiguredEventArgs applicationConfiguredEventArgs)
    {
        applicationConfiguredEventArgs = Guard.ValueIsNotNull(applicationConfiguredEventArgs, nameof(applicationConfiguredEventArgs));
        OnApplicationConfigured?.Invoke(this, applicationConfiguredEventArgs);
    }

    private void NotifyOnApplicationStarting(StartingApplicationEventArgs startingApplicationEventArgs)
    {
        startingApplicationEventArgs = Guard.ValueIsNotNull(startingApplicationEventArgs, nameof(startingApplicationEventArgs));
        OnApplicationStarting?.Invoke(this, startingApplicationEventArgs);
    }

    private void NotifyOnApplicationStarted(ApplicationStartedEventArgs applicationStartedEventArgs)
    {
        applicationStartedEventArgs = Guard.ValueIsNotNull(applicationStartedEventArgs, nameof(applicationStartedEventArgs));
        OnApplicationStarted?.Invoke(this, applicationStartedEventArgs);
    }
    #endregion

    public IWebAppStarter CreateWebApplicationBuilder(string[] applicationArgs, Action<WebApplicationBuilder> initializationAction)
    {
        _webApplicationBuilder = WebApplication.CreateBuilder(applicationArgs);
        initializationAction(_webApplicationBuilder);
        var configuration = _webApplicationBuilder.Configuration;
        Log.Logger = new LoggerConfiguration()
            .GetLoggerConfiguration(configuration)
            .CreateBootstrapLogger();
        NotifyOnInitializing(new InitializingEventArgs(configuration));
        _startupFeatureFactory = new StartupFeaturesFactory(configuration);
        var configureHostFeature = Guard.ValueIsNotNull(_startupFeatureFactory.GetConfigureHostStartupFeature(), "configureHostFeature");
        configureHostFeature.ConfigureHost(_webApplicationBuilder);
        NotifyOnInitialized(new InitializedEventArgs());
        return this;
    }

    public IWebAppStarterConfigurator ConfigureEventHandlers()
    {
        LoggingEventHandler.Instance.AttachEventHandlers();
        PerformanceMeasureEventHandler.Instance.AttachEventHandlers();
        return this;
    }

    public IConfigureWebAppBuilder ConfigureWebAppBuilder()
    {
        NotifyOnConfiguringServices(new ConfiguringServicesEventArgs());
        var configureServiceFeature = Guard.ValueIsNotNull(_startupFeatureFactory?.GetConfigureServicesStartupFeature(), "configureServiceFeature");
        _webApplicationBuilder = Guard.ValueIsNotNull(_webApplicationBuilder, nameof(_webApplicationBuilder));
        configureServiceFeature.ConfigureServices(_webApplicationBuilder);
        NotifyOnServicesConfigured(new ServicesConfiguredEventArgs());
        return this;
    }

    public IStartWebApp BuildWebApp()
    {
        _webApplicationBuilder = Guard.ValueIsNotNull(_webApplicationBuilder, nameof(_webApplicationBuilder));
        _webApplication = _webApplicationBuilder.Build();
        return this;
    }

    public IStartWebApp RunMigrations()
    {
        _webApplication = Guard.ValueIsNotNull(_webApplication, nameof(_webApplication));
        var migrationFeature = Guard.ValueIsNotNull(_startupFeatureFactory?.GetConfigureMigrationStartupFeature(), "migrationFeature");
        var migrationRunner = new MigrationRunner();
        migrationFeature.GetMigrations(migrationRunner);
        migrationRunner.RunMigrations(_webApplication);
        return this;
    }

    public IStartWebApp ConfigureRoutes()
    {
        _webApplication = Guard.ValueIsNotNull(_webApplication, nameof(_webApplication));
        var configureRoutes = Guard.ValueIsNotNull(_startupFeatureFactory?.GetConfigureRoutesStartupFeature(), "configureRoutes");
        configureRoutes.ConfigureRoutes(_webApplication);
        return this;
    }

    public IStartWebApp ConfigureApplication()
    {
        _webApplication = Guard.ValueIsNotNull(_webApplication, nameof(_webApplication));
        var configureAppServices = Guard.ValueIsNotNull(_startupFeatureFactory?.GetConfigureAppStartupFeature(), "configureAppServices");
        NotifyOnConfiguringApplication(new ConfiguringApplicationEventArgs());
        configureAppServices.ConfigureApplication(_webApplication);
        NotifyOnApplicationConfigured(new ApplicationConfiguredEventArgs());
        return this;
    }

    public Task RunApp(CancellationToken cancellationToken)
    {
        NotifyOnApplicationStarting(new StartingApplicationEventArgs());
        _webApplication = Guard.ValueIsNotNull(_webApplication, nameof(_webApplication));
        var webAppRunTask = _webApplication.RunAsync(cancellationToken);
        NotifyOnApplicationStarted(new ApplicationStartedEventArgs());
        return webAppRunTask;
    }
}
