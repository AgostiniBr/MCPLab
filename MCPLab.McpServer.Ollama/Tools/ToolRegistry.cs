// ------------------------------
// ETAPA 5 (Cria o Registro de ferramentas, e métodos para a chamada do MCP)
// ETAPA 6 IR PARA O ARQUIVO -> MCPLab.McpServer.Ollama.Tools.WeatherTool.cs
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

            //--> Registrar automaticamente todas as classes do tipo Tools
            //--> WeatherTool, GeneralTool, DeveloperTool
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
                //--> Verificar se os métodos das classes tipo Tools estão intanciados pela DI
                if (!_tools.TryGetValue(req.Method, out var method)) { return RpcResponse.Error(req.Id, "Método não encontrado"); }

                //--> Instanciar o Servico para saber qual classe do tipo Tool foi solicitada
                //--> O objeto tool vai ser preenchido com a informação que deve usar as possíveis classes do tipo Tool
                //--> WeatherTool, GeneralTool, DeveloperTool
                var toolType = method.DeclaringType!;
                var tool = _services.GetRequiredService(toolType);

                //--> Obtem o nome do parâmetro automaticamente
                //--> parâmetro como question, input, text
                var parameters = method.GetParameters();
                var paramName = parameters[0].Name;
                var paramValue = req.Params?
                    .GetProperty(paramName)
                    .GetString() ?? "";

                //--> Invoca o Método que foi obtido através da solicitação da classe tipo Tool
                //--> Passa juntamente o parâmetro que deve ser usado nessa classe do tipo Tool
                var task = (Task<string>)method.Invoke(tool, new object[] { paramValue, CancellationToken.None })!;

                //--> Preenche o objeto result
                var result = await task;

                //--> Retorna o objeto result
                return RpcResponse.Success(req.Id, result);
            }
            catch (Exception Ex) { return RpcResponse.Error(req.Id, Ex.Message); }
        }
    }
}