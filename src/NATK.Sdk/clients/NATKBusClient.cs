using NATK.Sdk;
using NATK.Sdk.Api.BusData.Resources;
using NATK.Sdk.Http;

namespace NATK.Clients.Sdk;

public sealed class NATKBusClient
{
    public BusLocationsResource BusLocations { get; }
    public NATKBusClient(NATKClientOptions options)
    {
        var handler = new ApiKeyAuthHandler(options.ApiKey)
        {
            InnerHandler = new HttpClientHandler()
        };
        var busClient = new HttpClient(handler)
        {
            BaseAddress = options.BaseUrl
        };
        BusLocations = new BusLocationsResource(busClient);
    }

    public NATKBusClient(HttpClient busClient)
    {
        BusLocations = new BusLocationsResource(busClient);
    }
}