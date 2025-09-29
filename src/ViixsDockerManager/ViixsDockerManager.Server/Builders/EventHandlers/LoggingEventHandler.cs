using System.Reflection;
using Microsoft.Extensions.Configuration;
using Serilog;
using ViixsDockerManager.Server.Builders.EventHandlers.EventArguments;
using ViixsDockerManager.Server.Helpers;

namespace ViixsDockerManager.Server.Builders.EventHandlers;

public class LoggingEventHandler : WebAppStarterEventHandler
{
    private static readonly Lazy<LoggingEventHandler> _loggingEventHandlerInstance = new Lazy<LoggingEventHandler>(() => new LoggingEventHandler());

    public static LoggingEventHandler Instance => _loggingEventHandlerInstance.Value;

    private Serilog.ILogger? _logger;
    private LoggingEventHandler()
    {
    }

    public override void AttachEventHandlers()
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


    public override void DetachEventHandlers()
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
        _logger.Information("Starting ViixsDockerManager {Version}", Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion[..14]);
        _logger.Information("Configuring WebAppStarter");
    }

    private void LogOnInitialized(object? sender, InitializedEventArgs e)
    {
        _logger?.Information("WebAppStarter configured");
    }

    private void LogOnConfiguringServices(object? sender, ConfiguringServicesEventArgs e) => _logger?.Information("Configuring services");
    private void LogOnServicesConfigured(object? sender, ServicesConfiguredEventArgs e) => _logger?.Information("Services configured");
    private void LogOnConfiguringApplication(object? sender, ConfiguringApplicationEventArgs e) => _logger?.Information("Configuring application");
    private void LogOnApplicationConfigured(object? sender, ApplicationConfiguredEventArgs e) => _logger?.Information("Application configured");
    private void LogOnApplicationStarting(object? sender, StartingApplicationEventArgs e)
    {
        _logger?.Information("Application Initialization Done.");
        _logger?.Information("Running ViixsDcokerManager");
    }

    private void LogOnApplicationStarted(object? sender, ApplicationStartedEventArgs e)
    {
        _logger?.Information("ViixsDockerManager is Started!");
        DetachEventHandlers();
    }
}
