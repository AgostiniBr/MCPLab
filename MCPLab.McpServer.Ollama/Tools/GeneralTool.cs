// ------------------------------
// ETAPA 6 -> (Método InvokeAync que com a ferramenta WeatherTool com cache + Ollama)
// ETAPA 7 IR PARA O ARQUIVO ->  MCPLab.McpServer.Ollama.Services.OllamaClient.cs
// ------------------------------

using MCPLab.McpServer.Ollama.Models;
using MCPLab.McpServer.Ollama.Services;
using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel;

namespace MCPLab.McpServer.Ollama.Tools
{
    [McpTool("general")]
    public class GeneralTool
    {
        //--> Dependency Injection
        private readonly IMemoryCache _cache;
        private readonly OllamaClient _ollama;

        //--> Constructor
        public GeneralTool(IMemoryCache cache, OllamaClient ollama)
        {
            _cache = cache;
            _ollama = ollama;
        }

        //--> Method Invoke Async para perguntas gerais
        [Description("Responde perguntas sobre informações gerais usando phi3 local e mantém cache.")]
        public async Task<string> InvokeAsync(
            [Description("Pergunta completa do usuário.")] string question, 
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (_cache.TryGetValue(question, out string? cached)) { return cached; }
                var prompt = $@"
Você é um assistente de perguntas sobre temas gerais.
Responda sempre de forma consistente.
A sua resposta deve ser curta e direta.
Pergunta: {question}
";
                var answer = await _ollama.GenerateAsync(prompt, cancellationToken);
                _cache.Set(question, answer, TimeSpan.FromMinutes(10));
                return answer;
            }
            catch (Exception) { return null; }
        }
    }
}
