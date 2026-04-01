using MCPLab.McpServer.Services;

var builder = WebApplication.CreateBuilder(args);
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

    return await WeatherTools.GetCurrentWeather(question);
});

app.Run("http://localhost:5050");

record WeatherQuestion(string Question);