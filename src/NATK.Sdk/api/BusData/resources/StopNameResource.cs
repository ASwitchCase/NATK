using System.Net.Http.Json;

namespace NATK.Sdk.Api.BusData.Resources;

/// <summary>
/// Provides access to bus stop name lookup data.
/// </summary>
public sealed class StopNameResource
{
    private readonly HttpClient _httpClient;

    public StopNameResource(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Retrieves the name of a bus stop by its stop number.
    /// </summary>
    /// <param name="stopNumber">The bus stop number to look up.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>The stop's name, or <c>null</c> if no data was returned.</returns>
    public async Task<StopNameModel?> GetStopNameAsync(string stopNumber, CancellationToken cancellationToken = default)
    {
        var formDataContent = new MultipartFormDataContent
        {
            { new StringContent(stopNumber), "stopnum" }
        };

        var response = await _httpClient.PostAsync("getStopName", formDataContent, cancellationToken);
        var body = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Bus API returned {(int)response.StatusCode}: {body}");
        }

        var stopName = await response.Content.ReadFromJsonAsync<StopName>(cancellationToken: cancellationToken);
        return stopName is null ? null : new StopNameModel
        {
            StopName = stopName.Value
        };
    }
}
