using System.Net.Http.Json;

namespace NATK.Sdk.Api.BusData.Resources;

public sealed class BusStopsResource
{
    private readonly HttpClient _httpClient;

    public BusStopsResource(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

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
