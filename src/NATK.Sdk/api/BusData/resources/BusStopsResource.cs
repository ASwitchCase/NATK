using System.Net.Http.Json;

namespace NATK.Sdk.Api.BusData.Resources;

/// <summary>
/// Provides access to bus stop data.
/// </summary>
public sealed class BusStopsResource
{
    private readonly HttpClient _httpClient;

    public BusStopsResource(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Retrieves bus stops, optionally filtered by route, direction, or name.
    /// </summary>
    /// <param name="route">The bus route identifier to filter results by, or <c>null</c> to include all routes.</param>
    /// <param name="direction">The route direction to filter results by, or <c>null</c> to include all directions.</param>
    /// <param name="nameContains">A substring to match against stop names, or <c>null</c> to skip name filtering.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>The matching bus stops, or an empty list if none were returned.</returns>
    public async Task<IReadOnlyList<BusStopModel>?> GetBusStopsAsync(
        string? route = null,
        string? direction = null,
        string? nameContains = null,
        CancellationToken cancellationToken = default)
    {
        var formDataContent = new MultipartFormDataContent();
        if (route is not null) formDataContent.Add(new StringContent(route), "route");
        if (direction is not null) formDataContent.Add(new StringContent(direction), "direction");
        if (nameContains is not null) formDataContent.Add(new StringContent(nameContains), "namecontains");

        var response = await _httpClient.PostAsync("getStops", formDataContent, cancellationToken);
        var body = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Bus API returned {(int)response.StatusCode}: {body}");
        }

        var busStops = await response.Content.ReadFromJsonAsync<BusStop[]>(cancellationToken: cancellationToken);
        return busStops?.Select(stop => new BusStopModel
        {
            BusStopDescription = stop.BusStopDescription,
            BusStopNumber = stop.BusStopNumber
        }).ToArray() ?? Array.Empty<BusStopModel>();
    }
}
