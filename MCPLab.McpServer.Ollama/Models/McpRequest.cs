using System.Text.Json;

namespace MCPLab.McpServer.Ollama.Models
{
    public class RpcRequest
    {
        public string Jsonrpc { get; set; } = "2.0";
        public string Method { get; set; } = "";
        public JsonElement? Params { get; set; }
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
