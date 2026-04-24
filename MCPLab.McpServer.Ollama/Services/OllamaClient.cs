// ------------------------------
// ETAPA 7 -> (Método GenerateAsync que faz a chamada para o modelo phi3 local usando Ollama)
// ------------------------------

namespace MCPLab.McpServer.Ollama.Services
{
    public class OllamaClient
    {
        private readonly HttpClient _http = new() { BaseAddress = new Uri("http://localhost:11434") };

        public async Task<string> GenerateAsync(string prompt, CancellationToken ct = default)
        {
            var req = new
            {
                model = "phi3:latest",
                prompt,
                stream = false
            };

            var res = await _http.PostAsJsonAsync("/api/generate", req, ct);
            res.EnsureSuccessStatusCode();

            var json = await res.Content.ReadFromJsonAsync<OllamaResponse>(cancellationToken: ct);
            return json?.response ?? "Sem resposta do modelo.";
        }
    }

    public class OllamaResponse
    {
        public string? response { get; set; }
    }
}
