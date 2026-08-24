using System.Text.Json.Serialization;

internal sealed class TrainStation {
    [JsonPropertyName("STATION_2CHAR")]
    public required string Station2Char { get; set; }

    [JsonPropertyName("STATIONNAME")]
    public required string StationName { get; set; }

    [JsonPropertyName("STATION_14CHAR")]
    public required string Station14Char { get; set; }

    [JsonPropertyName("WHEELCHAIR_ACCESSIBLE")]
    public required string WheelchairAccessible { get; set; }

}

public sealed class TrainStationModel {
    public required string Station2Char { get; set; }
    public required string StationName { get; set; }
    public required string Station14Char { get; set; }
    public required string WheelchairAccessible { get; set; }
}