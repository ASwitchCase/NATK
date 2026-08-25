using System.Text.Json.Serialization;

internal sealed class BusDirection
{
    [JsonPropertyName("Direction_1")]
    public string Direction1 { get; set; } = string.Empty;
    [JsonPropertyName("Direction_2")]
    public string Direction2 { get; set; } = string.Empty;
}

public sealed class BusDirectionModel
{
    public string Direction1 { get; set; } = string.Empty;
    public string Direction2 { get; set; } = string.Empty;
}
