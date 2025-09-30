using System.Reflection;
using Serilog;
using ViixsDockerManager.Server.Builders.EventHandlers.EventArguments;

namespace ViixsDockerManager.Server.Builders.EventHandlers;

public class LoggingEventHandler : WebAppStarterEventHandler
{
    private const string ApplicationName = "ViixsDockerManager";

    private static readonly Lazy<LoggingEventHandler> _loggingEventHandlerInstance = new Lazy<LoggingEventHandler>(() => new LoggingEventHandler());

    public static LoggingEventHandler Instance => _loggingEventHandlerInstance.Value;

    private Serilog.ILogger? _logger;
    private LoggingEventHandler()
    {
    }

    protected override void OnAttachEventHandlers()
    {
        WebAppStarter.Instance.OnInitializing += LogOnInitializing;
        WebAppStarter.Instance.OnInitialized += LogOnInitialized;
        WebAppStarter.Instance.OnConfiguringServices += LogOnConfiguringServices;
        WebAppStarter.Instance.OnServicesConfigured += LogOnServicesConfigured;
        WebAppStarter.Instance.OnConfiguringApplication += LogOnConfiguringApplication;
        WebAppStarter.Instance.OnApplicationConfigured += LogOnApplicationConfigured;
        WebAppStarter.Instance.OnApplicationStarting += LogOnApplicationStarting;
        WebAppStarter.Instance.OnApplicationStarted += LogOnApplicationStarted;
    }


    protected override void OnDetachEventHandlers()
    {
        WebAppStarter.Instance.OnInitializing -= LogOnInitializing;
        WebAppStarter.Instance.OnInitialized -= LogOnInitialized;
        WebAppStarter.Instance.OnConfiguringServices -= LogOnConfiguringServices;
        WebAppStarter.Instance.OnServicesConfigured -= LogOnServicesConfigured;
        WebAppStarter.Instance.OnConfiguringApplication -= LogOnConfiguringApplication;
        WebAppStarter.Instance.OnApplicationConfigured -= LogOnApplicationConfigured;
        WebAppStarter.Instance.OnApplicationStarted -= LogOnApplicationStarted;
    }

    private void LogOnInitializing(object? sender, InitializingEventArgs initializingEventArgs)
    {
        _logger = Log.Logger.ForContext<WebAppStarter>();
        _logger.Information("Starting {ApplicationName} {Version}", ApplicationName, Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion[..14]);
        _logger.Information("Configuring {ApplicationName} Builder", ApplicationName);
    }

    private void LogOnInitialized(object? sender, InitializedEventArgs e) => _logger?.Information("{ApplicationName} Builder configured", ApplicationName);
    private void LogOnConfiguringServices(object? sender, ConfiguringServicesEventArgs e) => _logger?.Information("Configuring {ApplicationName} services", ApplicationName);
    private void LogOnServicesConfigured(object? sender, ServicesConfiguredEventArgs e) => _logger?.Information("{ApplicationName} Services configured", ApplicationName);
    private void LogOnConfiguringApplication(object? sender, ConfiguringApplicationEventArgs e) => _logger?.Information("Configuring {ApplicationName}", ApplicationName);
    private void LogOnApplicationConfigured(object? sender, ApplicationConfiguredEventArgs e) => _logger?.Information("{ApplicationName} configured", ApplicationName);
    private void LogOnApplicationStarting(object? sender, StartingApplicationEventArgs e)
    {
        _logger?.Information("Initialization Done.");
        _logger?.Information("Running {ApplicationName}", ApplicationName);
    }

    private void LogOnApplicationStarted(object? sender, ApplicationStartedEventArgs e)
    {
        _logger?.Information("{ApplicationName} is Started!", ApplicationName);
    }
}
