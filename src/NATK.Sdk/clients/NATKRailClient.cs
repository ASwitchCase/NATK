using NATK.api.RailData.resources;
using NATK.Sdk;
using NATK.Sdk.Http;

namespace NATK.Clients.Sdk;
public sealed class NATKRailClient
{
    public TrainStationResource TrainStations { get; }

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
    }

    public NATKRailClient(HttpClient railClient)
    {
        TrainStations = new TrainStationResource(railClient);
    }
}