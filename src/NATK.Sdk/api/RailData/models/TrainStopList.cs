using System.Text.Json.Serialization;

internal sealed class TrainStopList
{
    [JsonPropertyName("TRAIN_ID")]
    public string TrainId { get; set; } = string.Empty;

    [JsonPropertyName("LINECODE")]
    public string LineCode { get; set; } = string.Empty;

    [JsonPropertyName("BACKCOLOR")]
    public string BackColor { get; set; } = string.Empty;

    [JsonPropertyName("FORECOLOR")]
    public string ForeColor { get; set; } = string.Empty;

    [JsonPropertyName("SHADOWCOLOR")]
    public string ShadowColor { get; set; } = string.Empty;

    [JsonPropertyName("DESTINATION")]
    public string Destination { get; set; } = string.Empty;

    [JsonPropertyName("TRANSFERAT")]
    public string TransferAt { get; set; } = string.Empty;

    [JsonPropertyName("STOPS")]
    public StopListItem[] Stops { get; set; } = Array.Empty<StopListItem>();

    [JsonPropertyName("CAPACITY")]
    public CapacityListItem[] Capacity { get; set; } = Array.Empty<CapacityListItem>();
}

public sealed class TrainStopListModel
{
    public string TrainId { get; set; } = string.Empty;
    public string LineCode { get; set; } = string.Empty;
    public string BackColor { get; set; } = string.Empty;
    public string ForeColor { get; set; } = string.Empty;
    public string ShadowColor { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public string TransferAt { get; set; } = string.Empty;
    public IReadOnlyList<StopListItemModel> Stops { get; set; } = Array.Empty<StopListItemModel>();
    public IReadOnlyList<CapacityListItemModel> Capacity { get; set; } = Array.Empty<CapacityListItemModel>();
}
