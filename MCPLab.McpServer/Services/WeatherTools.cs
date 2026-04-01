using MCPLab.McpServer.Models;
using System.Net.Http.Json;

namespace MCPLab.McpServer.Services;

public static class WeatherTools
{
    public static async Task<object> GetCurrentWeather(string question)
    {
        var city = ExtractCity(question);
        var coords = await GetCoordinates(city);

        var url =
            $"https://api.open-meteo.com/v1/forecast?latitude={coords.lat}&longitude={coords.lon}&current_weather=true";

        using var http = new HttpClient();
        var data = await http.GetFromJsonAsync<OpenMeteoResponse>(url);

        var temp = data?.weather[0].daily.temperature_2m_max;
        return new { Result = $"Clima atual em {city}: {temp}°C" };
    }

    public static async Task<object> GetForecast(string question)
    {
        var city = ExtractCity(question);
        var coords = await GetCoordinates(city);

        var url =
            $"https://api.open-meteo.com/v1/forecast?latitude={coords.lat}&longitude={coords.lon}&daily=temperature_2m_max,temperature_2m_min&forecast_days=1";

        using var http = new HttpClient();
        var data = await http.GetFromJsonAsync<OpenMeteoResponse>(url);

        var max = data?.Property1.?.FirstOrDefault();
        var min = data?.daily?.temperature_2m_min?.FirstOrDefault();

        return new { Result = $"Previsão para {city}: Máx {max}°C / Mín {min}°C" };
    }

    public static async Task<object> GetWind(string question)
    {
        var city = ExtractCity(question);
        var coords = await GetCoordinates(city);

        var url =
            $"https://api.open-meteo.com/v1/forecast?latitude={coords.lat}&longitude={coords.lon}&hourly=windspeed_10m&forecast_days=1";

        using var http = new HttpClient();
        var data = await http.GetFromJsonAsync<OpenMeteoResponse>(url);

        var wind = data?.hourly?.windspeed_10m?.FirstOrDefault();
        return new { Result = $"Vento em {city}: {wind} km/h" };
    }

    private static string ExtractCity(string question)
    {
        var parts = question.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return parts.LastOrDefault() ?? "São Paulo";
    }

    private static async Task<(double lat, double lon)> GetCoordinates(string city)
    {
        var url = $"https://geocoding-api.open-meteo.com/v1/search?name={Uri.EscapeDataString(city)}&count=1";
        using var http = new HttpClient();
        var data = await http.GetFromJsonAsync<GeoResponse>(url);

        var first = data?.results?.FirstOrDefault();
        if (first is null)
            return (-23.55, -46.63);

        return (first.latitude, first.longitude);
    }
}
