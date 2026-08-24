using MyCompany.Sdk.Http;
using NATK.Sdk.Api.BusData.Resources;

namespace NATK.Sdk;

public sealed class NATKClient
{
    public BusLocationsResource BusLocations { get; }
    public NATKClient(NATKClientOptions options)
    {
        var handler = new ApiKeyAuthHandler(options.ApiKey)
        {
            InnerHandler = new HttpClientHandler()
        };
        var busClient = new HttpClient(handler)
        {
            BaseAddress = options.BusBaseUrl
        };
        BusLocations = new BusLocationsResource(busClient);
    }

    internal NATKClient(HttpClient busClient)
    {
        BusLocations = new BusLocationsResource(busClient);
    }
}