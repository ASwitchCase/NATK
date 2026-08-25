using System.Text.Json.Serialization;

internal sealed class BusLocationData
{
    [JsonPropertyName("busstopdescription")]
    public string BusStopDescription { get; set; } = string.Empty;
    [JsonPropertyName("busstopnumber")]
    public string BusStopNumber { get; set; } = string.Empty;
    [JsonPropertyName("latitude")]
    public string Latitude { get; set; } = string.Empty;
    [JsonPropertyName("longitude")]
    public string Longitude { get; set; } = string.Empty;
    [JsonPropertyName("distance")]
    public string Distance { get; set; } = string.Empty;
    [JsonPropertyName("modetype")]
    public string ModeType { get; set; } = string.Empty;
}

public sealed class BusLocationDataModel
{
    public string BusStopDescription { get; set; } = string.Empty;
    public string BusStopNumber { get; set; } = string.Empty;
    public string Latitude { get; set; } = string.Empty;
    public string Longitude { get; set; } = string.Empty;
    public string Distance { get; set; } = string.Empty;
    public string ModeType { get; set; } = string.Empty;
}
