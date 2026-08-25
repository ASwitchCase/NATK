using System.Text.Json.Serialization;

internal sealed class DVTrip
{
    [JsonPropertyName("public_route")]
    public string PublicRoute { get; set; } = string.Empty;
    [JsonPropertyName("header")]
    public string Header { get; set; } = string.Empty;
    [JsonPropertyName("lanegate")]
    public string LaneGate { get; set; } = string.Empty;
    [JsonPropertyName("departuretime")]
    public string DepartureTime { get; set; } = string.Empty;
    [JsonPropertyName("departurestatus")]
    public string DepartureStatus { get; set; } = string.Empty;
    [JsonPropertyName("remarks")]
    public string Remarks { get; set; } = string.Empty;
    [JsonPropertyName("internal_trip_number")]
    public string InternalTripNumber { get; set; } = string.Empty;
    [JsonPropertyName("sched_dep_time")]
    public string SchedDepTime { get; set; } = string.Empty;
    [JsonPropertyName("timing_point_id")]
    public string TimingPointId { get; set; } = string.Empty;
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
    [JsonPropertyName("fullscreen")]
    public string FullScreen { get; set; } = string.Empty;
    [JsonPropertyName("passload")]
    public string PassLoad { get; set; } = string.Empty;
    [JsonPropertyName("vehicle_id")]
    public string VehicleId { get; set; } = string.Empty;
}

public sealed class DVTripModel
{
    public string PublicRoute { get; set; } = string.Empty;
    public string Header { get; set; } = string.Empty;
    public string LaneGate { get; set; } = string.Empty;
    public string DepartureTime { get; set; } = string.Empty;
    public string DepartureStatus { get; set; } = string.Empty;
    public string Remarks { get; set; } = string.Empty;
    public string InternalTripNumber { get; set; } = string.Empty;
    public string SchedDepTime { get; set; } = string.Empty;
    public string TimingPointId { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string FullScreen { get; set; } = string.Empty;
    public string PassLoad { get; set; } = string.Empty;
    public string VehicleId { get; set; } = string.Empty;
}
