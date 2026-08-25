using System.Net.Http.Json;

namespace NATK.Sdk.Api.BusData.Resources;

public sealed class RouteTripsResource
{
    private readonly HttpClient _httpClient;

    public RouteTripsResource(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyList<DVTripModel>?> GetRouteTripsAsync(
        string? location = null,
        string? route = null,
        CancellationToken cancellationToken = default)
    {
        var formDataContent = new MultipartFormDataContent();
        if (location is not null) formDataContent.Add(new StringContent(location), "location");
        if (route is not null) formDataContent.Add(new StringContent(route), "route");

        var response = await _httpClient.PostAsync("getRouteTrips", formDataContent, cancellationToken);
        var body = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Bus API returned {(int)response.StatusCode}: {body}");
        }

        var trips = await response.Content.ReadFromJsonAsync<DVTrip[]>(cancellationToken: cancellationToken);
        return trips?.Select(ToModel).ToArray() ?? Array.Empty<DVTripModel>();
    }

    internal static DVTripModel ToModel(DVTrip trip) => new()
    {
        PublicRoute = trip.PublicRoute,
        Header = trip.Header,
        LaneGate = trip.LaneGate,
        DepartureTime = trip.DepartureTime,
        DepartureStatus = trip.DepartureStatus,
        Remarks = trip.Remarks,
        InternalTripNumber = trip.InternalTripNumber,
        SchedDepTime = trip.SchedDepTime,
        TimingPointId = trip.TimingPointId,
        Message = trip.Message,
        FullScreen = trip.FullScreen,
        PassLoad = trip.PassLoad,
        VehicleId = trip.VehicleId
    };
}
