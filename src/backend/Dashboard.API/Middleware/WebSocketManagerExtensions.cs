namespace Dashboard.API.Middleware;

public static class WebSocketManagerExtensions
{
    public static IApplicationBuilder UseWebSocketManager(this IApplicationBuilder app, PathString path, WebSocketHandler handler)
    {
        return app.Map(path, _app => _app.UseMiddleware<WebSocketManagerMiddleware>(handler));
    }

    public static IServiceCollection AddWebSocketManager(this IServiceCollection services)
    {
        services.AddTransient<AnalyticsWebSocketHandler>();

        return services;
    }
}