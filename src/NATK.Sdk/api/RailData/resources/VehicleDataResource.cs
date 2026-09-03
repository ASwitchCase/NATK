using System.Net.Http.Json;

namespace NATK.Sdk.Api.RailData.Resources;

/// <summary>
/// Provides access to real-time train vehicle location data.
/// </summary>
public sealed class VehicleDataResource
{
    private readonly HttpClient _httpClient;

    public VehicleDataResource(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Retrieves real-time GPS location and status data for all trains.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>The vehicle location data, or an empty list if none were returned.</returns>
    public async Task<IReadOnlyList<VehicleDataModel>?> GetVehicleDataAsync(CancellationToken cancellationToken = default)
    {
        var formDataContent = new MultipartFormDataContent();

        var response = await _httpClient.PostAsync("getVehicleData", formDataContent, cancellationToken);
        var body = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Rail API returned {(int)response.StatusCode}: {body}");
        }

        var vehicles = await response.Content.ReadFromJsonAsync<VehicleData[]>(cancellationToken: cancellationToken);
        return vehicles?.Select(vehicle => new VehicleDataModel
        {
            Id = vehicle.Id,
            TrainLine = vehicle.TrainLine,
            Direction = vehicle.Direction,
            IcsTrackCkt = vehicle.IcsTrackCkt,
            LastModified = vehicle.LastModified,
            SchedDepTime = vehicle.SchedDepTime,
            SecLate = vehicle.SecLate,
            NextStop = vehicle.NextStop,
            Longitude = vehicle.Longitude,
            Latitude = vehicle.Latitude
        }).ToArray() ?? Array.Empty<VehicleDataModel>();
    }
}
