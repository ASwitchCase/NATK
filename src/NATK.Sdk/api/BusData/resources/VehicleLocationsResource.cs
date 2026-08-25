using System.Net.Http.Json;

namespace NATK.Sdk.Api.BusData.Resources;

public sealed class VehicleLocationsResource
{
    private readonly HttpClient _httpClient;

    public VehicleLocationsResource(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

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
