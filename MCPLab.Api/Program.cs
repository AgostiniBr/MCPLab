using MCPLab.Api.Services;

//--> Init Builder
var builder = WebApplication.CreateBuilder(args);

//--> Dependency Injection
//builder.Services.AddHttpClient<McpClient>();

//--> Dependency Injection
builder.Services.AddHttpClient<McpClientOllama>(c =>
{
    c.BaseAddress = new Uri("http://localhost:5005"); // Porta do MCP-like Server
});

//--> Add CORS
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

//--> Instance app
var app = builder.Build();
app.UseCors("AllowWeb");

//--> Endpoint MCP interno
//app.MapPost("/api/ask", async (AskRequest req, McpClient mcp) =>
//{
//    var answer = await mcp.CallToolAsync("route-weather", req.Question);
//    return new { answer };
//});

//--> Endpoint MCP Ollama
app.MapPost("/api/mcp/weather", async (AskRequest req, McpClientOllama mcp) =>
{
    var answer = await mcp.CallWeatherAsync(req.Question);
    return new { answer };
});

//--> Run app
app.Run();

#region INTERNAL METHOD
record AskRequest(string Question); 
#endregion