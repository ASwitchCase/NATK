using System.Net.Http.Json;

namespace NATK.Sdk.Api.BusData.Resources;

public sealed class BusDirectionsResource
{
    private readonly HttpClient _httpClient;

    public BusDirectionsResource(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

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
