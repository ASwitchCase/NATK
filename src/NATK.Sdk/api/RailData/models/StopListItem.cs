using System.Text.Json.Serialization;

internal sealed class StopLine
{
    [JsonPropertyName("LINE_CODE")]
    public string LineCode { get; set; } = string.Empty;

    [JsonPropertyName("LINE_NAME")]
    public string LineName { get; set; } = string.Empty;

    [JsonPropertyName("LINE_COLOR")]
    public string LineColor { get; set; } = string.Empty;
}

public sealed class StopLineModel
{
    public string LineCode { get; set; } = string.Empty;
    public string LineName { get; set; } = string.Empty;
    public string LineColor { get; set; } = string.Empty;
}

internal sealed class StopListItem
{
    [JsonPropertyName("STATION_2CHAR")]
    public string Station2Char { get; set; } = string.Empty;

    [JsonPropertyName("STATIONNAME")]
    public string StationName { get; set; } = string.Empty;

    [JsonPropertyName("TIME")]
    public string Time { get; set; } = string.Empty;

    [JsonPropertyName("PICKUP")]
    public string Pickup { get; set; } = string.Empty;

    [JsonPropertyName("DROPOFF")]
    public string Dropoff { get; set; } = string.Empty;

    [JsonPropertyName("DEPARTED")]
    public string Departed { get; set; } = string.Empty;

    [JsonPropertyName("STOP_STATUS")]
    public string StopStatus { get; set; } = string.Empty;

    [JsonPropertyName("DEP_TIME")]
    public string DepTime { get; set; } = string.Empty;

    [JsonPropertyName("TIME_UTC_FORMAT")]
    public string TimeUtcFormat { get; set; } = string.Empty;

    [JsonPropertyName("STOP_LINES")]
    public StopLine[] StopLines { get; set; } = Array.Empty<StopLine>();
}

public sealed class StopListItemModel
{
    public string Station2Char { get; set; } = string.Empty;
    public string StationName { get; set; } = string.Empty;
    public string Time { get; set; } = string.Empty;
    public string Pickup { get; set; } = string.Empty;
    public string Dropoff { get; set; } = string.Empty;
    public string Departed { get; set; } = string.Empty;
    public string StopStatus { get; set; } = string.Empty;
    public string DepTime { get; set; } = string.Empty;
    public string TimeUtcFormat { get; set; } = string.Empty;
    public IReadOnlyList<StopLineModel> StopLines { get; set; } = Array.Empty<StopLineModel>();
}
