namespace MCPLab.McpServer.Ollama.Models
{
    public class RpcResponse
    {
        public string Jsonrpc { get; set; } = "2.0";
        public string Id { get; set; }
        public object? Result { get; set; }
        public object? Exception { get; set; }

        public static RpcResponse Success(string id, object result)
            => new RpcResponse { Id = id, Result = result };

        public static RpcResponse Error(string id, string message)
            => new RpcResponse { Id = id, Result = new { message } };
    }
}
