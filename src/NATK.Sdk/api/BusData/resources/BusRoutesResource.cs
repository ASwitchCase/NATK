using System.Net.Http.Json;

namespace NATK.Sdk.Api.BusData.Resources;

/// <summary>
/// Provides access to bus route data.
/// </summary>
public sealed class BusRoutesResource
{
    private readonly HttpClient _httpClient;

    public BusRoutesResource(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Retrieves the list of bus routes.
    /// </summary>
    /// <param name="mode">The transit mode to filter results by. Defaults to <c>"ALL"</c>.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>The matching bus routes, or an empty list if none were returned.</returns>
    public async Task<IReadOnlyList<BusRouteModel>?> GetBusRoutesAsync(string mode = "ALL", CancellationToken cancellationToken = default)
    {
        var formDataContent = new MultipartFormDataContent
        {
            { new StringContent(mode), "mode" }
        };

        var response = await _httpClient.PostAsync("getBusRoutes", formDataContent, cancellationToken);
        var body = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Bus API returned {(int)response.StatusCode}: {body}");
        }

        var busRoutes = await response.Content.ReadFromJsonAsync<BusRoute[]>(cancellationToken: cancellationToken);
        return busRoutes?.Select(route => new BusRouteModel
        {
            BusRouteID = route.BusRouteID,
            BusRouteDescription = route.BusRouteDescription
        }).ToArray() ?? Array.Empty<BusRouteModel>();
    }
}
