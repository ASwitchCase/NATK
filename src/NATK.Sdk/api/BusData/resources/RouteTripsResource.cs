using System.Net.Http.Json;

namespace NATK.Sdk.Api.BusData.Resources;

/// <summary>
/// Provides access to bus trip data for a route.
/// </summary>
public sealed class RouteTripsResource
{
    private readonly HttpClient _httpClient;

    public RouteTripsResource(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Retrieves trips, optionally filtered by location or route.
    /// </summary>
    /// <param name="location">The location to filter trips by, or <c>null</c> to include all locations.</param>
    /// <param name="route">The bus route identifier to filter trips by, or <c>null</c> to include all routes.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>The matching trips, or an empty list if none were returned.</returns>
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

    /// <summary>
    /// Maps a raw <see cref="DVTrip"/> API response to a <see cref="DVTripModel"/>.
    /// </summary>
    /// <param name="trip">The raw trip data returned by the API.</param>
    /// <returns>The mapped trip model.</returns>
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
