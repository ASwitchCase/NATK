using System.Text.Json.Serialization;

internal sealed class TripStop
{
    [JsonPropertyName("TripNumber")]
    public string TripNumber { get; set; } = string.Empty;
    [JsonPropertyName("TimePoint")]
    public string TimePoint { get; set; } = string.Empty;
    [JsonPropertyName("Description")]
    public string Description { get; set; } = string.Empty;
    [JsonPropertyName("SchedLaneGate")]
    public string SchedLaneGate { get; set; } = string.Empty;
    [JsonPropertyName("ManLaneGate")]
    public string ManLaneGate { get; set; } = string.Empty;
    [JsonPropertyName("SchedDepTime")]
    public string SchedDepTime { get; set; } = string.Empty;
    [JsonPropertyName("ApproxTime")]
    public string ApproxTime { get; set; } = string.Empty;
    [JsonPropertyName("StopID")]
    public string StopId { get; set; } = string.Empty;
    [JsonPropertyName("Status")]
    public string Status { get; set; } = string.Empty;
}

public sealed class TripStopModel
{
    public string TripNumber { get; set; } = string.Empty;
    public string TimePoint { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string SchedLaneGate { get; set; } = string.Empty;
    public string ManLaneGate { get; set; } = string.Empty;
    public string SchedDepTime { get; set; } = string.Empty;
    public string ApproxTime { get; set; } = string.Empty;
    public string StopId { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}
