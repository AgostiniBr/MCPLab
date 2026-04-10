// ------------------------------
// ETAPA 5 -> Cria o Registro de ferramentas, e métodos para a chamada do MCP
// ETAPA 6 IR PARA O ARQUIVO -> MCPLab.Api.Services.Ollama.Tools.WeatherTool.cs
// ------------------------------

using MCPLab.McpServer.Ollama.Models;
using System.Reflection;

namespace MCPLab.McpServer.Ollama.Tools
{
    public class ToolRegistry
    {
        //--> Dependency Injection
        private readonly IServiceProvider _services;
        private readonly Dictionary<string, MethodInfo> _tools = new();

        //--> Constructor
        public ToolRegistry(IServiceProvider services)
        {
            _services = services;

            // Registrar automaticamente todas as tools
            var toolTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.GetCustomAttribute<McpToolAttribute>() != null);

            foreach (var type in toolTypes)
            {
                var attr = type.GetCustomAttribute<McpToolAttribute>()!;
                var method = type.GetMethod("InvokeAsync")!;
                _tools[attr.Name] = method;
            }
        }

        //--> Method Execute Async
        public async Task<RpcResponse> ExecuteAsync(RpcRequest req)
        {
            try
            {
                if (!_tools.TryGetValue(req.Method, out var method)) { return RpcResponse.Error(req.Id, "Método não encontrado"); }

                var toolType = method.DeclaringType!;
                var tool = _services.GetRequiredService(toolType);

                var param = req.Params?.GetProperty("question").GetString() ?? "";

                var task = (Task<string>)method.Invoke(tool, new object[] { param, CancellationToken.None })!;
                var result = await task;

                return RpcResponse.Success(req.Id, result);
            }
            catch (Exception Ex) { return RpcResponse.Error(req.Id, Ex.Message); }
        }
    }
}