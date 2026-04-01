var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<McpClient>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWeb",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});


var app = builder.Build();
app.UseCors("AllowWeb");

app.MapPost("/api/ask", async (AskRequest req, McpClient mcp) =>
{
    var answer = await mcp.CallToolAsync("route-weather", req.Question);
    return new { answer };
});

app.Run();

record AskRequest(string Question);