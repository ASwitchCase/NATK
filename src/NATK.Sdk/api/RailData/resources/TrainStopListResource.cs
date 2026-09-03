using System.Net.Http.Json;

namespace NATK.Sdk.Api.RailData.Resources;

/// <summary>
/// Provides access to per-train stop list and capacity data.
/// </summary>
public sealed class TrainStopListResource
{
    private readonly HttpClient _httpClient;

    public TrainStopListResource(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Retrieves the full stop list and capacity data for a specific train.
    /// </summary>
    /// <param name="train">The train identifier to look up.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>The train's stop list, or <c>null</c> if no data was returned.</returns>
    public async Task<TrainStopListModel?> GetTrainStopListAsync(string train, CancellationToken cancellationToken = default)
    {
        var formDataContent = new MultipartFormDataContent
        {
            { new StringContent(train), "train" }
        };

        var response = await _httpClient.PostAsync("getTrainStopList", formDataContent, cancellationToken);
        var body = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Rail API returned {(int)response.StatusCode}: {body}");
        }

        var stopList = await response.Content.ReadFromJsonAsync<TrainStopList>(cancellationToken: cancellationToken);
        return stopList is null ? null : new TrainStopListModel
        {
            TrainId = stopList.TrainId,
            LineCode = stopList.LineCode,
            BackColor = stopList.BackColor,
            ForeColor = stopList.ForeColor,
            ShadowColor = stopList.ShadowColor,
            Destination = stopList.Destination,
            TransferAt = stopList.TransferAt,
            Stops = stopList.Stops.Select(RailModelMappers.ToModel).ToArray(),
            Capacity = stopList.Capacity.Select(RailModelMappers.ToModel).ToArray()
        };
    }
}
