using System.Diagnostics;
using Humanizer;
using Serilog;
using Serilog.Core;

namespace ViixsDockerManager.Server.Builders.EventHandlers;

public class PerformanceMeasureEventHandler : WebAppStarterEventHandler
{
    private static readonly Lazy<PerformanceMeasureEventHandler> _performanceMeasureEventHandler = new Lazy<PerformanceMeasureEventHandler>(() => new PerformanceMeasureEventHandler());
    public static PerformanceMeasureEventHandler Instance => _performanceMeasureEventHandler.Value;

    private Stopwatch? _stopwatch;

    private PerformanceMeasureEventHandler()
    {

    }
    protected override void OnAttachEventHandlers()
    {
        WebAppStarter.Instance.OnInitializing += StartMeasureOnInitializing;
        WebAppStarter.Instance.OnApplicationStarted += StopMeasureOnApplicationStarted;
    }

    protected override void OnDetachEventHandlers()
    {
        WebAppStarter.Instance.OnInitializing -= StartMeasureOnInitializing;
        WebAppStarter.Instance.OnApplicationStarted -= StopMeasureOnApplicationStarted;
    }

    private void StartMeasureOnInitializing(object? sender, EventArguments.InitializingEventArgs e)
    {
        _stopwatch = Stopwatch.StartNew();
    }
    private void StopMeasureOnApplicationStarted(object? sender, EventArguments.ApplicationStartedEventArgs e)
    {
        if (_stopwatch is not null)
        {
            _stopwatch.Stop();
            var logger = Log.Logger.ForContext<WebAppStarter>();
            logger.Information("Application started in {ElaspedTime}", _stopwatch.Elapsed.Humanize(3));
        }
    }
}
