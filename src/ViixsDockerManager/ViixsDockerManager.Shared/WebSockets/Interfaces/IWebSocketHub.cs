namespace ViixsDockerManager.Shared.WebSockets.Interfaces;

/// <summary>
/// Marker interface to use our own <see cref="IConnectionRegistery{THub}" /> for managing connections.
///
/// If a hub is not marked with this interface, we cannot use the generic registery
/// </summary>
public interface IWebSocketHub
{
}
