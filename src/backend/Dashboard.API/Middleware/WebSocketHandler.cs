using System.Net.WebSockets;
using System.Text.Json;

namespace Dashboard.API.Middleware;

public abstract class WebSocketHandler
{
    protected WebSocketHandler()
    {
    }

    public virtual async Task OnConnected(WebSocket socket)
    {
    }

    public virtual async Task OnDisconnected(WebSocket socket)
    {
    }

    public async Task SendMessageAsync(WebSocket socket, object message)
    {
        if (socket.State != WebSocketState.Open)
            return;

        try
        {
            var serializedMessage = JsonSerializer.Serialize(message);
            var bytes = System.Text.Encoding.UTF8.GetBytes(serializedMessage);
            await socket.SendAsync(new ArraySegment<byte>(bytes, 0, bytes.Length), WebSocketMessageType.Text, true,
                CancellationToken.None);
        }
        catch (WebSocketException ex)
        {
            Console.WriteLine($"WebSocketException: {ex.Message}");
        }
        catch (ObjectDisposedException ex)
        {
            Console.WriteLine($"ObjectDisposedException: {ex.Message}");
        }
    }

    public async Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
    {
        while (socket.State == WebSocketState.Open)
        {
            result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        }
    }
}