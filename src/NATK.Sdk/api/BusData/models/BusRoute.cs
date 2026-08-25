using System.Text.Json.Serialization;

internal sealed class BusRoute
{
    [JsonPropertyName("BusRouteID")]
    public string BusRouteID { get; set; } = string.Empty;
    [JsonPropertyName("BusRouteDescription")]
    public string BusRouteDescription { get; set; } = string.Empty;
}

public sealed class BusRouteModel
{
    public string BusRouteID { get; set; } = string.Empty;
    public string BusRouteDescription { get; set; } = string.Empty;
}
