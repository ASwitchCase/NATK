using System.Net.Http.Json;

namespace NATK.Sdk.Api.BusData.Resources;

public sealed class BusDVResource
{
    private readonly HttpClient _httpClient;

    public BusDVResource(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<BusDVModel?> GetBusDVAsync(
        string? stop = null,
        string? direction = null,
        string? route = null,
        CancellationToken cancellationToken = default)
    {
        var formDataContent = new MultipartFormDataContent();
        if (stop is not null) formDataContent.Add(new StringContent(stop), "stop");
        if (direction is not null) formDataContent.Add(new StringContent(direction), "direction");
        if (route is not null) formDataContent.Add(new StringContent(route), "route");

        var response = await _httpClient.PostAsync("getBusDV", formDataContent, cancellationToken);
        var body = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Bus API returned {(int)response.StatusCode}: {body}");
        }

        var busDv = await response.Content.ReadFromJsonAsync<BusDVResponse>(cancellationToken: cancellationToken);
        return busDv is null ? null : new BusDVModel
        {
            Message = busDv.Message.Message,
            Trips = busDv.DVTrip.Select(RouteTripsResource.ToModel).ToArray()
        };
    }
}
