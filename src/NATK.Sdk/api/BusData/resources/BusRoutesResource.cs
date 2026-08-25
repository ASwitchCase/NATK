using System.Net.Http.Json;

namespace NATK.Sdk.Api.BusData.Resources;

public sealed class BusRoutesResource
{
    private readonly HttpClient _httpClient;

    public BusRoutesResource(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

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
