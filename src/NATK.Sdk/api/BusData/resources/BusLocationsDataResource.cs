using System.Net.Http.Json;

namespace NATK.Sdk.Api.BusData.Resources;

/// <summary>
/// Provides access to detailed bus stop location data, including geographic search.
/// </summary>
public sealed class BusLocationsDataResource
{
    private readonly HttpClient _httpClient;

    public BusLocationsDataResource(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Retrieves bus stop location data, optionally filtered by route, direction, geographic proximity, or mode.
    /// </summary>
    /// <param name="route">The bus route identifier to filter results by, or <c>null</c> to include all routes.</param>
    /// <param name="direction">The route direction to filter results by, or <c>null</c> to include all directions.</param>
    /// <param name="lat">The latitude to search near, or <c>null</c> to omit proximity filtering.</param>
    /// <param name="lon">The longitude to search near, or <c>null</c> to omit proximity filtering.</param>
    /// <param name="radius">The search radius (in miles) around the given <paramref name="lat"/>/<paramref name="lon"/>, or <c>null</c> for the default radius.</param>
    /// <param name="mode">The transit mode to filter results by. Defaults to <c>"ALL"</c>.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>The matching bus stop locations, or an empty list if none were returned.</returns>
    public async Task<IReadOnlyList<BusLocationDataModel>?> GetBusLocationsDataAsync(
        string? route = null,
        string? direction = null,
        string? lat = null,
        string? lon = null,
        int? radius = null,
        string mode = "ALL",
        CancellationToken cancellationToken = default)
    {
        var formDataContent = new MultipartFormDataContent
        {
            { new StringContent(mode), "mode" }
        };
        if (route is not null) formDataContent.Add(new StringContent(route), "route");
        if (direction is not null) formDataContent.Add(new StringContent(direction), "direction");
        if (lat is not null) formDataContent.Add(new StringContent(lat), "lat");
        if (lon is not null) formDataContent.Add(new StringContent(lon), "lon");
        if (radius is not null) formDataContent.Add(new StringContent(radius.Value.ToString()), "radius");

        var response = await _httpClient.PostAsync("getBusLocationsData", formDataContent, cancellationToken);
        var body = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Bus API returned {(int)response.StatusCode}: {body}");
        }

        var locations = await response.Content.ReadFromJsonAsync<BusLocationData[]>(cancellationToken: cancellationToken);
        return locations?.Select(location => new BusLocationDataModel
        {
            BusStopDescription = location.BusStopDescription,
            BusStopNumber = location.BusStopNumber,
            Latitude = location.Latitude,
            Longitude = location.Longitude,
            Distance = location.Distance,
            ModeType = location.ModeType
        }).ToArray() ?? Array.Empty<BusLocationDataModel>();
    }
}
