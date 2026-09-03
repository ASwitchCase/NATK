using System.Net.Http.Json;

namespace NATK.Sdk.Api.RailData.Resources;

/// <summary>
/// Provides access to station and line service message data.
/// </summary>
public sealed class StationMessagesResource
{
    private readonly HttpClient _httpClient;

    public StationMessagesResource(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Retrieves service messages, optionally filtered by station or line.
    /// </summary>
    /// <param name="station">The station code to filter messages by, or an empty string to include all stations.</param>
    /// <param name="line">The line to filter messages by, or an empty string to include all lines.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>The matching service messages, or an empty list if none were returned.</returns>
    public async Task<IReadOnlyList<StationMessageModel>?> GetStationMessagesAsync(
        string station = "",
        string line = "",
        CancellationToken cancellationToken = default)
    {
        var formDataContent = new MultipartFormDataContent
        {
            { new StringContent(station), "station" },
            { new StringContent(line), "line" }
        };

        var response = await _httpClient.PostAsync("getStationMSG", formDataContent, cancellationToken);
        var body = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Rail API returned {(int)response.StatusCode}: {body}");
        }

        var messages = await response.Content.ReadFromJsonAsync<StationMessage[]>(cancellationToken: cancellationToken);
        return messages?.Select(RailModelMappers.ToModel).ToArray() ?? Array.Empty<StationMessageModel>();
    }
}
