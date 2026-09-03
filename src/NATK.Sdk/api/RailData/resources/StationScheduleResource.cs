using System.Net.Http.Json;

namespace NATK.Sdk.Api.RailData.Resources;

/// <summary>
/// Provides access to daily station schedule data.
/// </summary>
public sealed class StationScheduleResource
{
    private readonly HttpClient _httpClient;

    public StationScheduleResource(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Retrieves the daily schedule for a station, optionally limited to NJ Transit-only trains.
    /// </summary>
    /// <param name="station">The station code to retrieve the schedule for, or an empty string to include all stations.</param>
    /// <param name="njtOnly">Whether to limit results to NJ Transit trains only.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>The matching daily station schedules, or an empty list if none were returned.</returns>
    public async Task<IReadOnlyList<DailyStationInfoModel>?> GetStationScheduleAsync(
        string station = "",
        bool njtOnly = false,
        CancellationToken cancellationToken = default)
    {
        var formDataContent = new MultipartFormDataContent
        {
            { new StringContent(station), "station" },
            { new StringContent(njtOnly ? "true" : "false"), "NJTOnly" }
        };

        var response = await _httpClient.PostAsync("getStationSchedule", formDataContent, cancellationToken);
        var body = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Rail API returned {(int)response.StatusCode}: {body}");
        }

        var schedules = await response.Content.ReadFromJsonAsync<DailyStationInfo[]>(cancellationToken: cancellationToken);
        return schedules?.Select(schedule => new DailyStationInfoModel
        {
            Station2Char = schedule.Station2Char,
            StationName = schedule.StationName,
            Items = schedule.Items.Select(item => new DailyScheduleInfoModel
            {
                SchedDepDate = item.SchedDepDate,
                Destination = item.Destination,
                Track = item.Track,
                Line = item.Line,
                TrainId = item.TrainId,
                ConnectingTrainId = item.ConnectingTrainId,
                StationPosition = item.StationPosition,
                Direction = item.Direction,
                DwellTime = item.DwellTime,
                PermPickup = item.PermPickup,
                PermDropoff = item.PermDropoff,
                StopCode = item.StopCode
            }).ToArray()
        }).ToArray() ?? Array.Empty<DailyStationInfoModel>();
    }
}
