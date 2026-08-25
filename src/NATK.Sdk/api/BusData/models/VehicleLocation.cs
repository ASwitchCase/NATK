using System.Text.Json.Serialization;

internal sealed class VehicleLocation
{
    [JsonPropertyName("VehicleLat")]
    public string VehicleLat { get; set; } = string.Empty;
    [JsonPropertyName("VehicleLong")]
    public string VehicleLong { get; set; } = string.Empty;
    [JsonPropertyName("VehicleID")]
    public string VehicleId { get; set; } = string.Empty;
    [JsonPropertyName("VehiclePassengerLoad")]
    public string VehiclePassengerLoad { get; set; } = string.Empty;
    [JsonPropertyName("VehicleRoute")]
    public string VehicleRoute { get; set; } = string.Empty;
    [JsonPropertyName("VehicleDestination")]
    public string VehicleDestination { get; set; } = string.Empty;
    [JsonPropertyName("VehicleDistanceMiles")]
    public string VehicleDistanceMiles { get; set; } = string.Empty;
    [JsonPropertyName("VehicleInternalTripNumber")]
    public string VehicleInternalTripNumber { get; set; } = string.Empty;
    [JsonPropertyName("VehicleScheduledDeparture")]
    public string VehicleScheduledDeparture { get; set; } = string.Empty;
    [JsonPropertyName("VehicleSecondsLate")]
    public string VehicleSecondsLate { get; set; } = string.Empty;
}

public sealed class VehicleLocationModel
{
    public string VehicleLat { get; set; } = string.Empty;
    public string VehicleLong { get; set; } = string.Empty;
    public string VehicleId { get; set; } = string.Empty;
    public string VehiclePassengerLoad { get; set; } = string.Empty;
    public string VehicleRoute { get; set; } = string.Empty;
    public string VehicleDestination { get; set; } = string.Empty;
    public string VehicleDistanceMiles { get; set; } = string.Empty;
    public string VehicleInternalTripNumber { get; set; } = string.Empty;
    public string VehicleScheduledDeparture { get; set; } = string.Empty;
    public string VehicleSecondsLate { get; set; } = string.Empty;
}
