using System.Text.Json.Serialization;

internal sealed class CarListItem
{
    [JsonPropertyName("CAR_NO")]
    public string CarNo { get; set; } = string.Empty;

    [JsonPropertyName("CAR_POSITION")]
    public string CarPosition { get; set; } = string.Empty;

    [JsonPropertyName("CAR_REST")]
    public bool CarRest { get; set; }

    [JsonPropertyName("CUR_PERCENTAGE")]
    public string CurPercentage { get; set; } = string.Empty;

    [JsonPropertyName("CUR_CAPACITY_COLOR")]
    public string CurCapacityColor { get; set; } = string.Empty;

    [JsonPropertyName("CUR_PASSENGER_COUNT")]
    public string CurPassengerCount { get; set; } = string.Empty;
}

public sealed class CarListItemModel
{
    public string CarNo { get; set; } = string.Empty;
    public string CarPosition { get; set; } = string.Empty;
    public bool CarRest { get; set; }
    public string CurPercentage { get; set; } = string.Empty;
    public string CurCapacityColor { get; set; } = string.Empty;
    public string CurPassengerCount { get; set; } = string.Empty;
}

internal sealed class SectionListItem
{
    [JsonPropertyName("SECTION_POSITION")]
    public string SectionPosition { get; set; } = string.Empty;

    [JsonPropertyName("CUR_PERCENTAGE")]
    public string CurPercentage { get; set; } = string.Empty;

    [JsonPropertyName("CUR_CAPACITY_COLOR")]
    public string CurCapacityColor { get; set; } = string.Empty;

    [JsonPropertyName("CUR_PASSENGER_COUNT")]
    public string CurPassengerCount { get; set; } = string.Empty;

    [JsonPropertyName("CARS")]
    public CarListItem[] Cars { get; set; } = Array.Empty<CarListItem>();
}

public sealed class SectionListItemModel
{
    public string SectionPosition { get; set; } = string.Empty;
    public string CurPercentage { get; set; } = string.Empty;
    public string CurCapacityColor { get; set; } = string.Empty;
    public string CurPassengerCount { get; set; } = string.Empty;
    public IReadOnlyList<CarListItemModel> Cars { get; set; } = Array.Empty<CarListItemModel>();
}

internal sealed class CapacityListItem
{
    [JsonPropertyName("VEHICLE_NO")]
    public string VehicleNo { get; set; } = string.Empty;

    [JsonPropertyName("LATITUDE")]
    public string Latitude { get; set; } = string.Empty;

    [JsonPropertyName("LONGITUDE")]
    public string Longitude { get; set; } = string.Empty;

    [JsonPropertyName("CREATED_TIME")]
    public string CreatedTime { get; set; } = string.Empty;

    [JsonPropertyName("VEHICLE_TYPE")]
    public string VehicleType { get; set; } = string.Empty;

    [JsonPropertyName("CUR_PERCENTAGE")]
    public string CurPercentage { get; set; } = string.Empty;

    [JsonPropertyName("CUR_CAPACITY_COLOR")]
    public string CurCapacityColor { get; set; } = string.Empty;

    [JsonPropertyName("CUR_PASSENGER_COUNT")]
    public string CurPassengerCount { get; set; } = string.Empty;

    [JsonPropertyName("PREV_PERCENTAGE")]
    public string PrevPercentage { get; set; } = string.Empty;

    [JsonPropertyName("PREV_CAPACITY_COLOR")]
    public string PrevCapacityColor { get; set; } = string.Empty;

    [JsonPropertyName("PREV_PASSENGER_COUNT")]
    public string PrevPassengerCount { get; set; } = string.Empty;

    [JsonPropertyName("SECTIONS")]
    public SectionListItem[] Sections { get; set; } = Array.Empty<SectionListItem>();
}

public sealed class CapacityListItemModel
{
    public string VehicleNo { get; set; } = string.Empty;
    public string Latitude { get; set; } = string.Empty;
    public string Longitude { get; set; } = string.Empty;
    public string CreatedTime { get; set; } = string.Empty;
    public string VehicleType { get; set; } = string.Empty;
    public string CurPercentage { get; set; } = string.Empty;
    public string CurCapacityColor { get; set; } = string.Empty;
    public string CurPassengerCount { get; set; } = string.Empty;
    public string PrevPercentage { get; set; } = string.Empty;
    public string PrevCapacityColor { get; set; } = string.Empty;
    public string PrevPassengerCount { get; set; } = string.Empty;
    public IReadOnlyList<SectionListItemModel> Sections { get; set; } = Array.Empty<SectionListItemModel>();
}
