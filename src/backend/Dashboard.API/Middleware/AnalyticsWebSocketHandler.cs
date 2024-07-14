using System.Net.WebSockets;
using Dashboard.API.Services;
using Dashboard.API.WebSocketHandlers;

namespace Dashboard.API.Middleware;

public class AnalyticsWebSocketHandler : WebSocketHandler
{
    private readonly AnalyticsService _analyticsService;

    public AnalyticsWebSocketHandler(AnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;

        _analyticsService.OnActiveUsersUpdated += async (activeUsers) =>
        {
            foreach (var socket in WebSocketConnections.Sockets)
            {
                await SendMessageAsync(socket, activeUsers);
            }
        };

        _analyticsService.OnTotalSalesUpdated += async (totalSales) =>
        {
            foreach (var socket in WebSocketConnections.Sockets)
            {
                await SendMessageAsync(socket, totalSales);
            }
        };

        _analyticsService.OnTopSellingProductsUpdated += async (products) =>
        {
            foreach (var socket in WebSocketConnections.Sockets)
            {
                await SendMessageAsync(socket, products);
            }
        };
    }

    public override async Task OnConnected(WebSocket socket)
    {
        WebSocketConnections.AddSocket(socket);
        await base.OnConnected(socket);
    }

    public override async Task OnDisconnected(WebSocket socket)
    {
        WebSocketConnections.RemoveSocket(socket);
        await base.OnDisconnected(socket);
    }
}