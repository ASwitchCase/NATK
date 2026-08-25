using System.Net.Http.Json;

namespace NATK.Sdk.Api.BusData.Resources;

/// <summary>
/// Provides access to bus terminal location data.
/// </summary>
public sealed class BusLocationsResource
{
    private readonly HttpClient _httpClient;

    public BusLocationsResource(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Retrieves the list of all bus terminals.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>The bus terminals, or an empty list if none were returned.</returns>
    public async Task<IReadOnlyList<BusLocationModel>?> GetBusLocationsAsync(CancellationToken cancellationToken = default)
    {
        var formDataContent = new MultipartFormDataContent
        {
            { new StringContent("ALL"), "mode" }
        };
        
        var response = await _httpClient.PostAsync("getLocations", formDataContent, cancellationToken);
        var body = await response.Content.ReadAsStringAsync(cancellationToken);
        
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Bus API returned {(int)response.StatusCode}: {body}");
        }

        var busLocations = await response.Content.ReadFromJsonAsync<BusLocation[]>(cancellationToken: cancellationToken);
        return busLocations?.Select(location => new BusLocationModel
        {
            BusTerminalCode = location.BusTerminalCode,
            BusTerminalName = location.BusTerminalName
        }).ToArray() ?? Array.Empty<BusLocationModel>();
    }
}