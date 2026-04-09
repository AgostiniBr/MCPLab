using MCPLab.McpServer.Ollama.Models;
using MCPLab.McpServer.Ollama.Services;
using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel;

namespace MCPLab.McpServer.Ollama.Tools
{
    [McpTool("weather")]
    public class WeatherTool
    {
        //--> Dependency Injection
        private readonly IMemoryCache _cache;
        private readonly OllamaClient _ollama;

        //--> Constructor
        public WeatherTool(IMemoryCache cache, OllamaClient ollama)
        {
            _cache = cache;
            _ollama = ollama;
        }


        //--> Method Invoke Async
        [Description("Responde perguntas sobre clima usando phi3 local e mantém cache.")]
        public async Task<string> InvokeAsync([Description("Pergunta completa do usuário.")] string question, CancellationToken cancellationToken = default)
        {
            try
            {
                if (_cache.TryGetValue(question, out string cached)) { return cached; }
                var prompt = $@"
Você é um assistente de clima. Responda sempre de forma consistente.
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
