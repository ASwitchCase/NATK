using System.Net.Http.Json;

namespace NATK.Sdk.Api.BusData.Resources;

/// <summary>
/// Provides access to bus route direction data.
/// </summary>
public sealed class BusDirectionsResource
{
    private readonly HttpClient _httpClient;

    public BusDirectionsResource(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Retrieves the available travel directions for a bus route.
    /// </summary>
    /// <param name="route">The bus route identifier to retrieve directions for.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>The route's direction names, or <c>null</c> if no data was returned.</returns>
    public async Task<BusDirectionModel?> GetBusDirectionsAsync(string route, CancellationToken cancellationToken = default)
    {
        var formDataContent = new MultipartFormDataContent
        {
            { new StringContent(route), "route" }
        };

        var response = await _httpClient.PostAsync("getBusDirectionsData", formDataContent, cancellationToken);
        var body = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Bus API returned {(int)response.StatusCode}: {body}");
        }

        var busDirections = await response.Content.ReadFromJsonAsync<BusDirection[]>(cancellationToken: cancellationToken);
        var busDirection = busDirections?.FirstOrDefault();
        return busDirection is null ? null : new BusDirectionModel
        {
            Direction1 = busDirection.Direction1,
            Direction2 = busDirection.Direction2
        };
    }
}
