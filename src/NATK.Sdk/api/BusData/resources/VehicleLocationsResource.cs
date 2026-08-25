using System.Net.Http.Json;

namespace NATK.Sdk.Api.BusData.Resources;

/// <summary>
/// Provides access to real-time bus vehicle location data.
/// </summary>
public sealed class VehicleLocationsResource
{
    private readonly HttpClient _httpClient;

    public VehicleLocationsResource(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Retrieves real-time vehicle locations, optionally filtered by geographic proximity and mode.
    /// </summary>
    /// <param name="lat">The latitude to search near, or <c>null</c> to omit proximity filtering.</param>
    /// <param name="lon">The longitude to search near, or <c>null</c> to omit proximity filtering.</param>
    /// <param name="radius">The search radius (in miles) around the given <paramref name="lat"/>/<paramref name="lon"/>. Defaults to <c>1</c>.</param>
    /// <param name="mode">The transit mode to filter results by. Defaults to <c>"ALL"</c>.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>The matching vehicle locations, or an empty list if none were returned.</returns>
    public async Task<IReadOnlyList<VehicleLocationModel>?> GetVehicleLocationsAsync(
        string? lat = null,
        string? lon = null,
        int radius = 1,
        string mode = "ALL",
        CancellationToken cancellationToken = default)
    {
        var formDataContent = new MultipartFormDataContent
        {
            { new StringContent(radius.ToString()), "radius" },
            { new StringContent(mode), "mode" }
        };
        if (lat is not null) formDataContent.Add(new StringContent(lat), "lat");
        if (lon is not null) formDataContent.Add(new StringContent(lon), "lon");

        var response = await _httpClient.PostAsync("getVehicleLocations", formDataContent, cancellationToken);
        var body = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Bus API returned {(int)response.StatusCode}: {body}");
        }

        var vehicles = await response.Content.ReadFromJsonAsync<VehicleLocation[]>(cancellationToken: cancellationToken);
        return vehicles?.Select(vehicle => new VehicleLocationModel
        {
            VehicleLat = vehicle.VehicleLat,
            VehicleLong = vehicle.VehicleLong,
            VehicleId = vehicle.VehicleId,
            VehiclePassengerLoad = vehicle.VehiclePassengerLoad,
            VehicleRoute = vehicle.VehicleRoute,
            VehicleDestination = vehicle.VehicleDestination,
            VehicleDistanceMiles = vehicle.VehicleDistanceMiles,
            VehicleInternalTripNumber = vehicle.VehicleInternalTripNumber,
            VehicleScheduledDeparture = vehicle.VehicleScheduledDeparture,
            VehicleSecondsLate = vehicle.VehicleSecondsLate
        }).ToArray() ?? Array.Empty<VehicleLocationModel>();
    }
}
