using System.Net.Http.Json;

namespace NATK.Sdk.Api.RailData.Resources;

/// <summary>
/// Provides access to real-time train schedule data for a station.
/// </summary>
public sealed class TrainScheduleResource
{
    private readonly HttpClient _httpClient;

    public TrainScheduleResource(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Retrieves the full real-time schedule/train board for a station.
    /// </summary>
    /// <param name="station">The station code to retrieve the schedule for.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>The station's schedule information, or <c>null</c> if no data was returned.</returns>
    public async Task<StationInfoModel?> GetTrainScheduleAsync(string station, CancellationToken cancellationToken = default)
    {
        var formDataContent = new MultipartFormDataContent
        {
            { new StringContent(station), "station" }
        };

        return await PostAndMapAsync("getTrainSchedule", formDataContent, cancellationToken);
    }

    /// <summary>
    /// Retrieves up to 19 upcoming departures for a station, optionally filtered by line.
    /// </summary>
    /// <param name="station">The station code to retrieve the schedule for.</param>
    /// <param name="line">The line to filter results by, or <c>null</c> to include all lines.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>The station's schedule information, or <c>null</c> if no data was returned.</returns>
    public async Task<StationInfoModel?> GetTrainScheduleByLineAsync(string station, string? line = null, CancellationToken cancellationToken = default)
    {
        var formDataContent = new MultipartFormDataContent
        {
            { new StringContent(station), "station" }
        };
        if (line is not null) formDataContent.Add(new StringContent(line), "line");

        return await PostAndMapAsync("getTrainSchedule19Rec", formDataContent, cancellationToken);
    }

    private async Task<StationInfoModel?> PostAndMapAsync(string requestUri, MultipartFormDataContent formDataContent, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsync(requestUri, formDataContent, cancellationToken);
        var body = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Rail API returned {(int)response.StatusCode}: {body}");
        }

        var stationInfo = await response.Content.ReadFromJsonAsync<StationInfo>(cancellationToken: cancellationToken);
        return stationInfo is null ? null : new StationInfoModel
        {
            Station2Char = stationInfo.Station2Char,
            StationName = stationInfo.StationName,
            StationMsgs = stationInfo.StationMsgs.Select(RailModelMappers.ToModel).ToArray(),
            Items = stationInfo.Items.Select(item => new ScheduleInfoModel
            {
                SchedDepDate = item.SchedDepDate,
                Destination = item.Destination,
                Track = item.Track,
                Line = item.Line,
                TrainId = item.TrainId,
                ConnectingTrainId = item.ConnectingTrainId,
                Status = item.Status,
                SecLate = item.SecLate,
                LastModified = item.LastModified,
                BackColor = item.BackColor,
                ForeColor = item.ForeColor,
                ShadowColor = item.ShadowColor,
                GpsLatitude = item.GpsLatitude,
                GpsLongitude = item.GpsLongitude,
                GpsTime = item.GpsTime,
                StationPosition = item.StationPosition,
                LineCode = item.LineCode,
                LineAbbreviation = item.LineAbbreviation,
                InlineMsg = item.InlineMsg,
                Capacity = item.Capacity.Select(RailModelMappers.ToModel).ToArray(),
                Stops = item.Stops.Select(RailModelMappers.ToModel).ToArray()
            }).ToArray()
        };
    }
}
