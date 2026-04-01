using MCPLab.McpServer.Models;

namespace MCPLab.McpServer.Services;
public static class WeatherTools
{
    #region METHOD GET CURRENT WEATHER
    public static async Task<object> GetCurrentWeather(string question)
    {
        var city = ExtractCity(question);
        var coords = await GetCoordinates(city);

        var url =
            $"https://api.open-meteo.com/v1/forecast?latitude={coords.lat.ToString().Replace(",",".")}&longitude={coords.lon.ToString().Replace(",", ".")}&current_weather=true";

        using var http = new HttpClient();
        var data = await http.GetFromJsonAsync<GetCurrentWeatherResponse>(url);

        var temp = data?.current_weather.temperature;
        return new { Result = $"Clima atual em {city}: {temp}°C" };
    }
    #endregion

    #region METHOD GET FORECAST
    public static async Task<object> GetForecast(string question)
    {
        var city = ExtractCity(question);
        var coords = await GetCoordinates(city);

        var url =
            $"https://api.open-meteo.com/v1/forecast?latitude={coords.lat.ToString().Replace(",", ".")}&longitude={coords.lon.ToString().Replace(",", ".")}&daily=temperature_2m_max,temperature_2m_min&forecast_days=1";

        using var http = new HttpClient();
        var data = await http.GetFromJsonAsync<GetForecastResponse>(url);

        var max = data?.daily.temperature_2m_max;
        var min = data?.daily.temperature_2m_min;

        return new { Result = $"Previsão para {city}: Máx {max[0]}°C / Mín {min[0]}°C" };
    }
    #endregion

    #region METHOD GET WIND
    public static async Task<object> GetWind(string question)
    {
        var city = ExtractCity(question);
        var coords = await GetCoordinates(city);

        var url =
            $"https://api.open-meteo.com/v1/forecast?latitude={coords.lat.ToString().Replace(",", ".")}&longitude={coords.lon.ToString().Replace(",", ".")}&hourly=windspeed_10m&forecast_days=1";

        using var http = new HttpClient();
        var data = await http.GetFromJsonAsync<GetWindResponse>(url);
        float wind = 0;
        var HoraAtual = DateTime.UtcNow.Hour;
        for (int i = 0; i < data.hourly.time.Length; i++)
        {
            var HoraApi = Convert.ToDateTime(data.hourly.time[i]).TimeOfDay.Hours;
            if (HoraApi.Equals(HoraAtual)) { wind = data.hourly.windspeed_10m[i]; break; } 
        }
        return new { Result = $"Vento em {city}: {wind} km/h" };
    }
    #endregion

    #region METHOD EXTRACT CITY
    private static string ExtractCity(string question)
    {
        var parts = question.Split('-', StringSplitOptions.RemoveEmptyEntries);
        return parts[0];
    }
    #endregion

    #region METHOD GET COORDINATES
    private static async Task<(double lat, double lon)> GetCoordinates(string city)
    {
        var url = $"https://geocoding-api.open-meteo.com/v1/search?name={Uri.EscapeDataString(city)}&count=1";
        using var http = new HttpClient();
        var data = await http.GetFromJsonAsync<GeoResponse>(url);

        var first = data?.results?.FirstOrDefault();
        if (first is null) { return (-23.55, -46.63); }
        
        return (first.latitude, first.longitude);
    } 
    #endregion
}