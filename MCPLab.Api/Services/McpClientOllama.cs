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

        //--> Method Call Weather Async
        public async Task<string> CallWeatherAsync(string question)
        {
            try
            {
                var rpcRequest = new
                {
                    jsonrpc = "2.0",
                    method = "weather",
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
