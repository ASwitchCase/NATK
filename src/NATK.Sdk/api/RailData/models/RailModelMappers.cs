internal static class RailModelMappers
{
    internal static StationMessageModel ToModel(StationMessage message) => new()
    {
        MsgType = message.MsgType,
        MsgText = message.MsgText,
        MsgRichText = message.MsgRichText,
        MsgPubDate = message.MsgPubDate,
        MsgId = message.MsgId,
        MsgAgency = message.MsgAgency,
        MsgSource = message.MsgSource,
        MsgStationScope = message.MsgStationScope,
        MsgLineScope = message.MsgLineScope,
        MsgPubDateUtc = message.MsgPubDateUtc,
        MsgUrl = message.MsgUrl
    };

    internal static StopLineModel ToModel(StopLine line) => new()
    {
        LineCode = line.LineCode,
        LineName = line.LineName,
        LineColor = line.LineColor
    };

    internal static StopListItemModel ToModel(StopListItem stop) => new()
    {
        Station2Char = stop.Station2Char,
        StationName = stop.StationName,
        Time = stop.Time,
        Pickup = stop.Pickup,
        Dropoff = stop.Dropoff,
        Departed = stop.Departed,
        StopStatus = stop.StopStatus,
        DepTime = stop.DepTime,
        TimeUtcFormat = stop.TimeUtcFormat,
        StopLines = stop.StopLines.Select(ToModel).ToArray()
    };

    internal static CarListItemModel ToModel(CarListItem car) => new()
    {
        CarNo = car.CarNo,
        CarPosition = car.CarPosition,
        CarRest = car.CarRest,
        CurPercentage = car.CurPercentage,
        CurCapacityColor = car.CurCapacityColor,
        CurPassengerCount = car.CurPassengerCount
    };

    internal static SectionListItemModel ToModel(SectionListItem section) => new()
    {
        SectionPosition = section.SectionPosition,
        CurPercentage = section.CurPercentage,
        CurCapacityColor = section.CurCapacityColor,
        CurPassengerCount = section.CurPassengerCount,
        Cars = section.Cars.Select(ToModel).ToArray()
    };

    internal static CapacityListItemModel ToModel(CapacityListItem capacity) => new()
    {
        VehicleNo = capacity.VehicleNo,
        Latitude = capacity.Latitude,
        Longitude = capacity.Longitude,
        CreatedTime = capacity.CreatedTime,
        VehicleType = capacity.VehicleType,
        CurPercentage = capacity.CurPercentage,
        CurCapacityColor = capacity.CurCapacityColor,
        CurPassengerCount = capacity.CurPassengerCount,
        PrevPercentage = capacity.PrevPercentage,
        PrevCapacityColor = capacity.PrevCapacityColor,
        PrevPassengerCount = capacity.PrevPassengerCount,
        Sections = capacity.Sections.Select(ToModel).ToArray()
    };
}
