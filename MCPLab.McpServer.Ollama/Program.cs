// ------------------------------
// ETAPA 4 (enviar para o Endpoint MCP-like usando o protocolo JSON-RPC)
// ETAPA 5 IR PARA O ARQUIVO -> MCPLab.Api.Services.Ollama.Tools.ToolRegistry.cs
// ------------------------------

using MCPLab.McpServer.Ollama.Models;
using MCPLab.McpServer.Ollama.Services;
using MCPLab.McpServer.Ollama.Tools;

//--> Init Builder
var builder = WebApplication.CreateBuilder(args);

//--> Dependency Injection
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<OllamaClient>();
builder.Services.AddSingleton<ToolRegistry>();
builder.Services.AddSingleton<MCPLab.McpServer.Ollama.Tools.WeatherTool>();
builder.Services.AddSingleton<MCPLab.McpServer.Ollama.Tools.GeneralTool>();
builder.Services.AddSingleton<MCPLab.McpServer.Ollama.Tools.DeveloperTool>();


//--> Instance app
var app = builder.Build();

//--> Endpoint MCP-like (JSON-RPC)
app.MapPost("/mcp", async (RpcRequest req, ToolRegistry registry) =>
{
    var result = await registry.ExecuteAsync(req);
    return Results.Json(result);
});

//--> Run app
app.Run("http://localhost:5005");