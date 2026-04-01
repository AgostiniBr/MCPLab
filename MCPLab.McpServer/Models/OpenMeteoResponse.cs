namespace MCPLab.McpServer.Models;

public class OpenMeteoResponse
{
    public RootObject[] weather { get; set; }
}

public class RootObject
{
    public int latitude { get; set; }
    public int longitude { get; set; }
    public float generationtime_ms { get; set; }
    public int utc_offset_seconds { get; set; }
    public string timezone { get; set; }
    public string timezone_abbreviation { get; set; }
    public int elevation { get; set; }
    public Daily_Units daily_units { get; set; }
    public Daily daily { get; set; }
    public int location_id { get; set; }
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