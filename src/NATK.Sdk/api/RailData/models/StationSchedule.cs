using System.Text.Json.Serialization;

internal sealed class DailyScheduleInfo
{
    [JsonPropertyName("SCHED_DEP_DATE")]
    public string SchedDepDate { get; set; } = string.Empty;

    [JsonPropertyName("DESTINATION")]
    public string Destination { get; set; } = string.Empty;

    [JsonPropertyName("TRACK")]
    public string Track { get; set; } = string.Empty;

    [JsonPropertyName("LINE")]
    public string Line { get; set; } = string.Empty;

    [JsonPropertyName("TRAIN_ID")]
    public string TrainId { get; set; } = string.Empty;

    [JsonPropertyName("CONNECTING_TRAIN_ID")]
    public string ConnectingTrainId { get; set; } = string.Empty;

    [JsonPropertyName("STATION_POSITION")]
    public string StationPosition { get; set; } = string.Empty;

    [JsonPropertyName("DIRECTION")]
    public string Direction { get; set; } = string.Empty;

    [JsonPropertyName("DWELL_TIME")]
    public string DwellTime { get; set; } = string.Empty;

    [JsonPropertyName("PERM_PICKUP")]
    public string PermPickup { get; set; } = string.Empty;

    [JsonPropertyName("PERM_DROPOFF")]
    public string PermDropoff { get; set; } = string.Empty;

    [JsonPropertyName("STOP_CODE")]
    public string StopCode { get; set; } = string.Empty;
}

public sealed class DailyScheduleInfoModel
{
    public string SchedDepDate { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public string Track { get; set; } = string.Empty;
    public string Line { get; set; } = string.Empty;
    public string TrainId { get; set; } = string.Empty;
    public string ConnectingTrainId { get; set; } = string.Empty;
    public string StationPosition { get; set; } = string.Empty;
    public string Direction { get; set; } = string.Empty;
    public string DwellTime { get; set; } = string.Empty;
    public string PermPickup { get; set; } = string.Empty;
    public string PermDropoff { get; set; } = string.Empty;
    public string StopCode { get; set; } = string.Empty;
}

internal sealed class DailyStationInfo
{
    [JsonPropertyName("STATION_2CHAR")]
    public string Station2Char { get; set; } = string.Empty;

    [JsonPropertyName("STATIONNAME")]
    public string StationName { get; set; } = string.Empty;

    [JsonPropertyName("ITEMS")]
    public DailyScheduleInfo[] Items { get; set; } = Array.Empty<DailyScheduleInfo>();
}

public sealed class DailyStationInfoModel
{
    public string Station2Char { get; set; } = string.Empty;
    public string StationName { get; set; } = string.Empty;
    public IReadOnlyList<DailyScheduleInfoModel> Items { get; set; } = Array.Empty<DailyScheduleInfoModel>();
}
