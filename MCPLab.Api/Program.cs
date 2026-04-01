
#region BUILDER CONSTRUCT
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient<McpClient>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWeb",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});
#endregion

#region APP CONSTRUCT
var app = builder.Build();
app.UseCors("AllowWeb");
app.MapPost("/api/ask", async (AskRequest req, McpClient mcp) =>
{
    var answer = await mcp.CallToolAsync("route-weather", req.Question);
    return new { answer };
});
app.Run();
#endregion

#region INTERNAL METHOD
record AskRequest(string Question); 
#endregion