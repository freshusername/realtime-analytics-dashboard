using Dashboard.API.Middleware;
using Dashboard.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddWebSocketManager();
builder.Services.AddSingleton<AnalyticsService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", builder =>
    {
        builder.WithOrigins("http://localhost:3000", "http://localhost")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowLocalhost");

var webSocketOptions = new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromMinutes(2),
    AllowedOrigins = { "http://localhost:3000", "http://localhost" }
};
app.UseWebSockets(webSocketOptions);
var analyticsWebSocketHandler = builder.Services.BuildServiceProvider().GetRequiredService<AnalyticsWebSocketHandler>();
app.UseWebSocketManager("/ws", analyticsWebSocketHandler);

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();