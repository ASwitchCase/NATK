using System.Net.Http.Json;

namespace NATK.Sdk.Api.BusData.Resources;

public sealed class StopNameResource
{
    private readonly HttpClient _httpClient;

    public StopNameResource(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

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
