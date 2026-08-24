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
        var response = await _httpClient.GetAsync("bus-locations", cancellationToken);
        response.EnsureSuccessStatusCode();

        var busLocations = await response.Content.ReadFromJsonAsync<BusLocation[]>(cancellationToken: cancellationToken);
        return busLocations ?? Array.Empty<BusLocation>();
    }
}