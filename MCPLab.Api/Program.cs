// ------------------------------
// ETAPA 2 (Obter a pergunta e mandar para api Ollama)
// ETAPA 3 IR PARA O ARQUIVO -> MCPLab.Api.Services.McpClientOllama.cs
// ------------------------------

using MCPLab.Api.Services;

//--> Init Builder
var builder = WebApplication.CreateBuilder(args);

//--> Dependency Injection
//builder.Services.AddHttpClient<McpClient>();

//--> Dependency Injection for Ollama
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

#region Endpoint MCP Simples
//--> Descomentar esse código caso não for usar o Ollama ou outro agente
//app.MapPost("/api/ask", async (AskRequest req, McpClient mcp) =>
//{
//    var answer = await mcp.CallToolAsync("route-weather", req.Question);
//    return new { answer };
//}); 
#endregion

#region Endpoint MCP Ollama
/* Este servidor:
    - expõe /mcp via POST (JSON-RPC)
    - registra ferramentas
    - executa ferramentas dinamicamente
    - retorna respostas no formato MCP-like
 */
app.MapPost("/api/mcp/ollama", async (AskRequest req, McpClientOllama mcp) =>
{
    string question = req.Question.ToLower();
    string method = string.Empty;
    string answer = string.Empty;

    //--> Regra simples e básica para um roteamento
    //--> Serve apenas para demonstrar que é possível rotear o tipo de pergunta para um enpoint específico
    //--> Pode e deve-se criar um "RouterTool" mas elaborado
    if (question.Contains("clima") || question.Contains("tempo") || question.Contains("chuva")
    || question.Contains("vento") || question.Contains("previsão") || question.Contains("temperatura")
    || question.Contains("frio") || question.Contains("calor")) { answer = await mcp.CallAgentAIAsync("weather", req.Question); }
    //--//
    if (question.Contains("erro") || question.Contains("código") || question.Contains("programação")
    || question.Contains("c#") || question.Contains("desenvolvimento") || question.Contains("linguagens")) { answer = await mcp.CallAgentAIAsync("developer", req.Question); }
    //--//
    else { answer = await mcp.CallAgentAIAsync("general", req.Question); }
    //--//
    return new { answer };
}); 
#endregion

//--> Run app
app.Run();

record AskRequest(string Question);