using MCPLab.Api;

public class McpClient
{
    #region VARIABLES
    private readonly HttpClient _http;
    #endregion

    #region CONSTRUCT
    public McpClient(HttpClient http)
    {
        _http = http;
    }
    #endregion

    #region METHOD CALL TOOL ASYNC
    public async Task<string> CallToolAsync(string toolName, string question)
    {
        var response = await _http.PostAsJsonAsync(
            $"http://localhost:5050/tools/{toolName}",
            new { question });

        var result = await response.Content.ReadFromJsonAsync<McpResponse>();
        return result?.Result ?? "Erro ao consultar MCP";
    } 
    #endregion
}