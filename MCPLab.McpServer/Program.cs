using MCPLab.McpServer.Services;

#region BUILDER CONSTRUCT
var builder = WebApplication.CreateBuilder(args); 
#endregion

#region APP CONSTRUCT
var app = builder.Build();
app.MapPost("/tools/route-weather", async (WeatherQuestion input) =>
{
    var question = input.Question ?? "";

    if (question.Contains("amanhã", StringComparison.OrdinalIgnoreCase) ||
        question.Contains("previsão", StringComparison.OrdinalIgnoreCase))
    {
        return await WeatherTools.GetForecast(question);
    }

    if (question.Contains("vento", StringComparison.OrdinalIgnoreCase))
    {
        return await WeatherTools.GetWind(question);
    }

    if (question.Contains("hoje", StringComparison.OrdinalIgnoreCase))
    {
        return await WeatherTools.GetCurrentWeather(question);
    }

    return await WeatherTools.GetCurrentWeather(question);
});
app.Run("http://localhost:5050"); 
#endregion

#region INTERNAL METHOD
record WeatherQuestion(string Question); 
#endregion