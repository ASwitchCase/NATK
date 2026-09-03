using NATK.Sdk;
using NATK.Sdk.Api.RailData.Resources;
using NATK.Sdk.Http;

namespace NATK.Clients.Sdk;
public sealed class NATKRailClient
{
    public TrainStationResource TrainStations { get; }
    public StationMessagesResource StationMessages { get; }
    public StationScheduleResource StationSchedule { get; }
    public TrainScheduleResource TrainSchedule { get; }
    public TrainStopListResource TrainStopList { get; }
    public VehicleDataResource VehicleData { get; }

    public NATKRailClient(NATKClientOptions options)
    {
        var handler = new ApiKeyAuthHandler(options.ApiKey)
        {
            InnerHandler = new HttpClientHandler()
        };
        var railClient = new HttpClient(handler)
        {
            BaseAddress = options.BaseUrl
        };
        TrainStations = new TrainStationResource(railClient);
        StationMessages = new StationMessagesResource(railClient);
        StationSchedule = new StationScheduleResource(railClient);
        TrainSchedule = new TrainScheduleResource(railClient);
        TrainStopList = new TrainStopListResource(railClient);
        VehicleData = new VehicleDataResource(railClient);
    }

    public NATKRailClient(HttpClient railClient)
    {
        TrainStations = new TrainStationResource(railClient);
        StationMessages = new StationMessagesResource(railClient);
        StationSchedule = new StationScheduleResource(railClient);
        TrainSchedule = new TrainScheduleResource(railClient);
        TrainStopList = new TrainStopListResource(railClient);
        VehicleData = new VehicleDataResource(railClient);
    }
}