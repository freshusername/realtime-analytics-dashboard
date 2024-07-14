using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace Dashboard.API.WebSocketHandlers;

public static class WebSocketConnections
{
    public static ConcurrentBag<WebSocket> Sockets { get; } = [];

    public static void AddSocket(WebSocket socket)
    {
        Sockets.Add(socket);
    }

    public static void RemoveSocket(WebSocket socket)
    {
        Sockets.TryTake(out socket);
    }
}