using System.Text.Json.Serialization;

internal sealed class BusLocation
{
    [JsonPropertyName("bus_terminal_code")]
    public string BusTerminalCode { get; set; } = string.Empty;
    [JsonPropertyName("bus_terminal_name")]
    public string BusTerminalName { get; set; } = string.Empty;
}

public sealed class BusLocationModel
{
    public string BusTerminalCode { get; set; } = string.Empty;
    public string BusTerminalName { get; set; } = string.Empty;
}