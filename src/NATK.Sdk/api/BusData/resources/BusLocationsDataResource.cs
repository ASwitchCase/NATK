using System.Net.Http.Json;

namespace NATK.Sdk.Api.BusData.Resources;

public sealed class BusLocationsDataResource
{
    private readonly HttpClient _httpClient;

    public BusLocationsDataResource(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyList<BusLocationDataModel>?> GetBusLocationsDataAsync(
        string? route = null,
        string? direction = null,
        string? lat = null,
        string? lon = null,
        int? radius = null,
        string mode = "ALL",
        CancellationToken cancellationToken = default)
    {
        var formDataContent = new MultipartFormDataContent
        {
            { new StringContent(mode), "mode" }
        };
        if (route is not null) formDataContent.Add(new StringContent(route), "route");
        if (direction is not null) formDataContent.Add(new StringContent(direction), "direction");
        if (lat is not null) formDataContent.Add(new StringContent(lat), "lat");
        if (lon is not null) formDataContent.Add(new StringContent(lon), "lon");
        if (radius is not null) formDataContent.Add(new StringContent(radius.Value.ToString()), "radius");

        var response = await _httpClient.PostAsync("getBusLocationsData", formDataContent, cancellationToken);
        var body = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Bus API returned {(int)response.StatusCode}: {body}");
        }

        var locations = await response.Content.ReadFromJsonAsync<BusLocationData[]>(cancellationToken: cancellationToken);
        return locations?.Select(location => new BusLocationDataModel
        {
            BusStopDescription = location.BusStopDescription,
            BusStopNumber = location.BusStopNumber,
            Latitude = location.Latitude,
            Longitude = location.Longitude,
            Distance = location.Distance,
            ModeType = location.ModeType
        }).ToArray() ?? Array.Empty<BusLocationDataModel>();
    }
}
