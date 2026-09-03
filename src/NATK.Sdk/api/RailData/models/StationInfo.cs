using System.Text.Json.Serialization;

internal sealed class ScheduleInfo
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

    [JsonPropertyName("STATUS")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("SEC_LATE")]
    public string SecLate { get; set; } = string.Empty;

    [JsonPropertyName("LAST_MODIFIED")]
    public string LastModified { get; set; } = string.Empty;

    [JsonPropertyName("BACKCOLOR")]
    public string BackColor { get; set; } = string.Empty;

    [JsonPropertyName("FORECOLOR")]
    public string ForeColor { get; set; } = string.Empty;

    [JsonPropertyName("SHADOWCOLOR")]
    public string ShadowColor { get; set; } = string.Empty;

    [JsonPropertyName("GPSLATITUDE")]
    public string GpsLatitude { get; set; } = string.Empty;

    [JsonPropertyName("GPSLONGITUDE")]
    public string GpsLongitude { get; set; } = string.Empty;

    [JsonPropertyName("GPSTIME")]
    public string GpsTime { get; set; } = string.Empty;

    [JsonPropertyName("STATION_POSITION")]
    public string StationPosition { get; set; } = string.Empty;

    [JsonPropertyName("LINECODE")]
    public string LineCode { get; set; } = string.Empty;

    [JsonPropertyName("LINEABBREVIATION")]
    public string LineAbbreviation { get; set; } = string.Empty;

    [JsonPropertyName("INLINEMSG")]
    public string InlineMsg { get; set; } = string.Empty;

    [JsonPropertyName("CAPACITY")]
    public CapacityListItem[] Capacity { get; set; } = Array.Empty<CapacityListItem>();

    [JsonPropertyName("STOPS")]
    public StopListItem[] Stops { get; set; } = Array.Empty<StopListItem>();
}

public sealed class ScheduleInfoModel
{
    public string SchedDepDate { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public string Track { get; set; } = string.Empty;
    public string Line { get; set; } = string.Empty;
    public string TrainId { get; set; } = string.Empty;
    public string ConnectingTrainId { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string SecLate { get; set; } = string.Empty;
    public string LastModified { get; set; } = string.Empty;
    public string BackColor { get; set; } = string.Empty;
    public string ForeColor { get; set; } = string.Empty;
    public string ShadowColor { get; set; } = string.Empty;
    public string GpsLatitude { get; set; } = string.Empty;
    public string GpsLongitude { get; set; } = string.Empty;
    public string GpsTime { get; set; } = string.Empty;
    public string StationPosition { get; set; } = string.Empty;
    public string LineCode { get; set; } = string.Empty;
    public string LineAbbreviation { get; set; } = string.Empty;
    public string InlineMsg { get; set; } = string.Empty;
    public IReadOnlyList<CapacityListItemModel> Capacity { get; set; } = Array.Empty<CapacityListItemModel>();
    public IReadOnlyList<StopListItemModel> Stops { get; set; } = Array.Empty<StopListItemModel>();
}

internal sealed class StationInfo
{
    [JsonPropertyName("STATION_2CHAR")]
    public string Station2Char { get; set; } = string.Empty;

    [JsonPropertyName("STATIONNAME")]
    public string StationName { get; set; } = string.Empty;

    [JsonPropertyName("STATIONMSGS")]
    public StationMessage[] StationMsgs { get; set; } = Array.Empty<StationMessage>();

    [JsonPropertyName("ITEMS")]
    public ScheduleInfo[] Items { get; set; } = Array.Empty<ScheduleInfo>();
}

public sealed class StationInfoModel
{
    public string Station2Char { get; set; } = string.Empty;
    public string StationName { get; set; } = string.Empty;
    public IReadOnlyList<StationMessageModel> StationMsgs { get; set; } = Array.Empty<StationMessageModel>();
    public IReadOnlyList<ScheduleInfoModel> Items { get; set; } = Array.Empty<ScheduleInfoModel>();
}
