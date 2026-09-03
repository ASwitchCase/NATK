using System.Net.Http.Json;

namespace NATK.Sdk.Api.RailData.Resources;

/// <summary>
/// Provides access to rail station data.
/// </summary>
public sealed class TrainStationResource
{
    private readonly HttpClient _httpClient;

    public TrainStationResource(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Retrieves the list of all available train stations.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>The train stations, or an empty list if none were returned.</returns>
    public async Task<IReadOnlyList<TrainStationModel>?> GetTrainStationsAsync(CancellationToken cancellationToken = default)
    {
        var formDataContent = new MultipartFormDataContent{};
        
        var response = await _httpClient.PostAsync("getStationList", formDataContent, cancellationToken);
        var body = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Rail API returned {(int)response.StatusCode}: {body}");
        }

        var trainStations = await response.Content.ReadFromJsonAsync<TrainStation[]>(cancellationToken: cancellationToken);
        return trainStations?.Select(station => new TrainStationModel
        {
            Station2Char = station.Station2Char,
            StationName = station.StationName,
            Station14Char = station.Station14Char,
            WheelchairAccessible = station.WheelchairAccessible
        }).ToArray() ?? Array.Empty<TrainStationModel>();
    }
}