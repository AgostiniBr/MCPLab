// ------------------------------
// ETAPA 3 -> essa etapa existe pois não utilizei o
// -> SDK ModelContextProtocol da empresa Anthropic - existe no NUGET
// -> devido a isso é necessário criar um MCP-like Server “compatível”
// -> usando Minimal API + JSON-RPC + Tools (com "Description" e "Parâmetros") + Cache + Ollama
// -> a estrutura do projeto como um todo será assim
// -> MCPLab.McpServer/
// ->   Program.cs
// ->     Tools/
// ->       WeatherTool.cs
// ->     Services/
// ->       OllamaClient.cs
// ->     Models/
// ->       RpcRequest.cs
// ->       RpcResponse.cs

// ETAPA 4 IR PARA O ARQUIVO -> MCPLab.McpServer.Ollama.Program.cs
// ------------------------------

using System.Text.Json;
namespace MCPLab.Api.Services
{
    public class McpClientOllama
    {
        //--> Dependency Injection
        private readonly HttpClient _http;

        //--> Constructor
        public McpClientOllama(HttpClient http)
        {
            _http = http;
        }

        //--> Method Call Agent AI Async
        public async Task<string> CallAgentAIAsync(string _method, string question)
        {
            try
            {
                var rpcRequest = new
                {
                    jsonrpc = "2.0",
                    method = _method,
                    @params = new { question },
                    id = Guid.NewGuid().ToString()
                };

                var response = await _http.PostAsJsonAsync("/mcp", rpcRequest);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadFromJsonAsync<JsonElement>();

                if (json.TryGetProperty("result", out var result)) { return result.GetString() ?? "Sem resposta"; }

                if (json.TryGetProperty("error", out var error)) { return error.GetProperty("message").GetString() ?? "Erro desconhecido"; }

                return "Resposta inválida do MCP.";
            }
            catch (Exception) { return string.Empty; }
        }
    }
}