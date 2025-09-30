using Serilog;

namespace ViixsDockerManager.Server.Builders.EventHandlers;

public abstract class WebAppStarterEventHandler : IDisposable
{
    private bool _disposedValue;
    private bool _isDetached;

    public void AttachEventHandlers()
    {
        OnAttachEventHandlers();      
    }

    public void DetachEventHandlers()
    {
        OnDetachEventHandlers();
        _isDetached = true;
    }

    protected abstract void OnAttachEventHandlers();
    protected abstract void OnDetachEventHandlers();

    protected virtual void Dispose(bool disposing)
    {
        if (_disposedValue)
        {
            return;
        }

        if(!_isDetached)
        {
            var logger = Log.Logger.ForContext<WebAppStarter>();
            logger.Warning("Event handlers were not detached before disposing {EventHandlerType}. Did you call {DetachEventHandlers} ?", GetType().FullName, nameof(DetachEventHandlers));
        }

        _disposedValue = true;
    }
    ~WebAppStarterEventHandler()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
