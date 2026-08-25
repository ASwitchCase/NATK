using System.Text.Json.Serialization;

internal sealed class BusDVMessage
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}

internal sealed class BusDVResponse
{
    [JsonPropertyName("message")]
    public BusDVMessage Message { get; set; } = new();
    [JsonPropertyName("DVTrip")]
    public DVTrip[] DVTrip { get; set; } = Array.Empty<DVTrip>();
}

public sealed class BusDVModel
{
    public string Message { get; set; } = string.Empty;
    public IReadOnlyList<DVTripModel> Trips { get; set; } = Array.Empty<DVTripModel>();
}
