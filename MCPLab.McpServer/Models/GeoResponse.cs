namespace MCPLab.McpServer.Models;

public class GeoResponse
{
    public List<GeoResult>? results { get; set; } = new List<GeoResult>();
}

public class GeoResult
{
    public double latitude { get; set; } = 0;
    public double longitude { get; set; } = 0;
}