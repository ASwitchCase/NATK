using System.Text.Json.Serialization;

internal sealed class VehicleData
{
    [JsonPropertyName("ID")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("TRAIN_LINE")]
    public string TrainLine { get; set; } = string.Empty;

    [JsonPropertyName("DIRECTION")]
    public string Direction { get; set; } = string.Empty;

    [JsonPropertyName("ICS_TRACK_CKT")]
    public string IcsTrackCkt { get; set; } = string.Empty;

    [JsonPropertyName("LAST_MODIFIED")]
    public string LastModified { get; set; } = string.Empty;

    [JsonPropertyName("SCHED_DEP_TIME")]
    public string SchedDepTime { get; set; } = string.Empty;

    [JsonPropertyName("SEC_LATE")]
    public string SecLate { get; set; } = string.Empty;

    [JsonPropertyName("NEXT_STOP")]
    public string NextStop { get; set; } = string.Empty;

    [JsonPropertyName("LONGITUDE")]
    public string Longitude { get; set; } = string.Empty;

    [JsonPropertyName("LATITUDE")]
    public string Latitude { get; set; } = string.Empty;
}

public sealed class VehicleDataModel
{
    public string Id { get; set; } = string.Empty;
    public string TrainLine { get; set; } = string.Empty;
    public string Direction { get; set; } = string.Empty;
    public string IcsTrackCkt { get; set; } = string.Empty;
    public string LastModified { get; set; } = string.Empty;
    public string SchedDepTime { get; set; } = string.Empty;
    public string SecLate { get; set; } = string.Empty;
    public string NextStop { get; set; } = string.Empty;
    public string Longitude { get; set; } = string.Empty;
    public string Latitude { get; set; } = string.Empty;
}
