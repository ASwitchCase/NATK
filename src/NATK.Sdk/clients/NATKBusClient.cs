using NATK.Sdk;
using NATK.Sdk.Api.BusData.Resources;
using NATK.Sdk.Http;

namespace NATK.Clients.Sdk;

public sealed class NATKBusClient
{
    public BusLocationsResource BusLocations { get; }
    public BusRoutesResource BusRoutes { get; }
    public BusDirectionsResource BusDirections { get; }
    public BusStopsResource BusStops { get; }
    public RouteTripsResource RouteTrips { get; }
    public StopNameResource StopName { get; }
    public BusLocationsDataResource BusLocationsData { get; }
    public BusDVResource BusDV { get; }
    public TripStopsResource TripStops { get; }
    public VehicleLocationsResource VehicleLocations { get; }

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
        BusRoutes = new BusRoutesResource(busClient);
        BusDirections = new BusDirectionsResource(busClient);
        BusStops = new BusStopsResource(busClient);
        RouteTrips = new RouteTripsResource(busClient);
        StopName = new StopNameResource(busClient);
        BusLocationsData = new BusLocationsDataResource(busClient);
        BusDV = new BusDVResource(busClient);
        TripStops = new TripStopsResource(busClient);
        VehicleLocations = new VehicleLocationsResource(busClient);
    }

    public NATKBusClient(HttpClient busClient)
    {
        BusLocations = new BusLocationsResource(busClient);
        BusRoutes = new BusRoutesResource(busClient);
        BusDirections = new BusDirectionsResource(busClient);
        BusStops = new BusStopsResource(busClient);
        RouteTrips = new RouteTripsResource(busClient);
        StopName = new StopNameResource(busClient);
        BusLocationsData = new BusLocationsDataResource(busClient);
        BusDV = new BusDVResource(busClient);
        TripStops = new TripStopsResource(busClient);
        VehicleLocations = new VehicleLocationsResource(busClient);
    }
}