namespace ViixsDockerManager.Shared.WebSockets.Interfaces;

public interface IConnectionRegistery<THub>
    where THub: class, IWebSocketHub
{
    void AddOrUpdateConnectionId(string key, string connectionId);
    void RemoveConnectionId(string connectionId);
    string? GetConnectionId(string key);
}
