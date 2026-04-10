// ------------------------------
// ETAPA 6 -> (Método InvokeAync que com a ferramenta WeatherTool com cache + Ollama)
// ETAPA 7 -> Retorna a resposta do agente
// ------------------------------

using MCPLab.McpServer.Ollama.Models;
using MCPLab.McpServer.Ollama.Services;
using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel;

namespace MCPLab.McpServer.Ollama.Tools
{
    [McpTool("developer")]
    public class DeveloperTool
    {
        //--> Dependency Injection
        private readonly IMemoryCache _cache;
        private readonly OllamaClient _ollama;

        //--> Constructor
        public DeveloperTool(IMemoryCache cache, OllamaClient ollama)
        {
            _cache = cache;
            _ollama = ollama;
        }

        //--> Method Invoke Async para perguntas sobre desenvolvimento de software
        [Description("Responde perguntas sobre informações de desenvolvimento usando phi3 local e mantém cache.")]
        public async Task<string> InvokeAsync(
            [Description("Pergunta completa do usuário.")] string question, 
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (_cache.TryGetValue(question, out string? cached)) { return cached; }
                var prompt = $@"
Você é um assistente de perguntas sobre temas de desenvolvimento de software.
Responda sempre de forma consistente e preferencialmente baseados no ecossitema .NET da Microsoft.
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
