public class McpClient
{
    private readonly HttpClient _http;

    public McpClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<string> CallToolAsync(string toolName, string question)
    {
        var response = await _http.PostAsJsonAsync(
            $"http://localhost:5050/tools/{toolName}",
            new { question });

        var result = await response.Content.ReadFromJsonAsync<McpResponse>();
        return result?.Result ?? "Erro ao consultar MCP";
    }
}

public class McpResponse
{
    public string? Result { get; set; } = string.Empty;
}