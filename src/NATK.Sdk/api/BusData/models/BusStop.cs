using System.Text.Json.Serialization;

internal sealed class BusStop
{
    [JsonPropertyName("busstopdescription")]
    public string BusStopDescription { get; set; } = string.Empty;
    [JsonPropertyName("busstopnumber")]
    public string BusStopNumber { get; set; } = string.Empty;
}

public sealed class BusStopModel
{
    public string BusStopDescription { get; set; } = string.Empty;
    public string BusStopNumber { get; set; } = string.Empty;
}
