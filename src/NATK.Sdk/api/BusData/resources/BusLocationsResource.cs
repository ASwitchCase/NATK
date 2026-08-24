using System.Net.Http.Json;

namespace NATK.Sdk.Api.BusData.Resources;

public sealed class BusLocationsResource
{
    private readonly HttpClient _httpClient;

    public BusLocationsResource(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyList<BusLocation>> GetBusLocationsAsync(CancellationToken cancellationToken = default)
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
        return busLocations ?? Array.Empty<BusLocation>();
    }
}