using System.Net.Http.Json;

namespace NATK.Sdk.Api.BusData.Resources;

/// <summary>
/// Provides access to the bus departure/arrival "DV" (digital view) sign data endpoint.
/// </summary>
public sealed class BusDVResource
{
    private readonly HttpClient _httpClient;

    public BusDVResource(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Retrieves digital sign (departure/arrival) trip data, optionally filtered by stop, direction, or route.
    /// </summary>
    /// <param name="stop">The bus stop number to filter results by, or <c>null</c> to include all stops.</param>
    /// <param name="direction">The route direction to filter results by, or <c>null</c> to include all directions.</param>
    /// <param name="route">The bus route identifier to filter results by, or <c>null</c> to include all routes.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>The digital sign data, including a status message and matching trips, or <c>null</c> if no data was returned.</returns>
    public async Task<BusDVModel?> GetBusDVAsync(
        string? stop = null,
        string? direction = null,
        string? route = null,
        CancellationToken cancellationToken = default)
    {
        var formDataContent = new MultipartFormDataContent();
        if (stop is not null) formDataContent.Add(new StringContent(stop), "stop");
        if (direction is not null) formDataContent.Add(new StringContent(direction), "direction");
        if (route is not null) formDataContent.Add(new StringContent(route), "route");

        var response = await _httpClient.PostAsync("getBusDV", formDataContent, cancellationToken);
        var body = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Bus API returned {(int)response.StatusCode}: {body}");
        }

        var busDv = await response.Content.ReadFromJsonAsync<BusDVResponse>(cancellationToken: cancellationToken);
        return busDv is null ? null : new BusDVModel
        {
            Message = busDv.Message.Message,
            Trips = busDv.DVTrip.Select(RouteTripsResource.ToModel).ToArray()
        };
    }
}
