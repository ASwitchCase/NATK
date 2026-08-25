using System.Text.Json.Serialization;

internal sealed class StopName
{
    [JsonPropertyName("stopName")]
    public string Value { get; set; } = string.Empty;
}

public sealed class StopNameModel
{
    public string StopName { get; set; } = string.Empty;
}
