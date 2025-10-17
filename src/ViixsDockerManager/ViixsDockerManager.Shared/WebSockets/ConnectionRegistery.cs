using System.Collections.Concurrent;
using ViixsDockerManager.Shared.WebSockets.Interfaces;

namespace ViixsDockerManager.Shared.WebSockets;

public class ConnectionRegistery<THub> : IConnectionRegistery<THub>
    where THub : class, IWebSocketHub
{
    private readonly ConcurrentDictionary<string, string> _connectionDictionary = new ConcurrentDictionary<string, string>();

    public void AddOrUpdateConnectionId(string key, string connectionId)
    {
        _connectionDictionary.AddOrUpdate(key, connectionId, (key, oldValue) => connectionId);
    }

    public void RemoveConnectionId(string connectionId)
    {
        var connection = _connectionDictionary.FirstOrDefault(c => c.Value == connectionId);
        if (string.IsNullOrEmpty(connection.Key))
        {
            return;
        }
        _connectionDictionary.TryRemove(connection.Key, out _);
    }

    public string? GetConnectionId(string key) => _connectionDictionary.TryGetValue(key, out var connectionId) ? connectionId : null;
}
