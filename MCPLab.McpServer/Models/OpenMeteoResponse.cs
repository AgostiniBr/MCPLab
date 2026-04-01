namespace MCPLab.McpServer.Models;

public class GetCurrentWeatherResponse
{
    public float latitude { get; set; }
    public float longitude { get; set; }
    public float generationtime_ms { get; set; }
    public int utc_offset_seconds { get; set; }
    public string timezone { get; set; }
    public string timezone_abbreviation { get; set; }
    public decimal elevation { get; set; }
    public Current_Weather_Units current_weather_units { get; set; }
    public Current_Weather current_weather { get; set; }
}

public class GetForecastResponse
{
    public float latitude { get; set; }
    public float longitude { get; set; }
    public float generationtime_ms { get; set; }
    public int utc_offset_seconds { get; set; }
    public string timezone { get; set; }
    public string timezone_abbreviation { get; set; }
    public decimal elevation { get; set; }
    public Daily_Units daily_units { get; set; }
    public Daily daily { get; set; }
}

public class GetWindResponse
{
    public float latitude { get; set; }
    public float longitude { get; set; }
    public float generationtime_ms { get; set; }
    public int utc_offset_seconds { get; set; }
    public string timezone { get; set; }
    public string timezone_abbreviation { get; set; }
    public decimal elevation { get; set; }
    public Hourly_units hourly_units { get; set; }
    public Hourly hourly { get; set; }
}

public class Daily_Units
{
    public string time { get; set; }
    public string temperature_2m_max { get; set; }
    public string temperature_2m_min { get; set; }
}

public class Daily
{
    public string[] time { get; set; }
    public float[] temperature_2m_max { get; set; }
    public float[] temperature_2m_min { get; set; }
}

public class Hourly_units
{ 
    public string time { get; set; }
    public string windspeed_10m { get; set; }
}

public class Hourly
{ 
    public string[] time { get; set; }
    public float[] windspeed_10m { get; set; }
}

public class Current_Weather_Units
{
    public string time { get; set; }
    public string interval { get; set; }
    public string temperature { get; set; }
    public string windspeed { get; set; }
    public string winddirection { get; set; }
    public string is_day { get; set; }
    public string weathercode { get; set; }
}

public class Current_Weather
{
    public string time { get; set; }
    public decimal interval { get; set; }
    public float temperature { get; set; }
    public float windspeed { get; set; }
    public int winddirection { get; set; }
    public int is_day { get; set; }
    public int weathercode { get; set; }
}
