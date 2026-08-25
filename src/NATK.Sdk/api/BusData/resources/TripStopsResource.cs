using System.Net.Http.Json;

namespace NATK.Sdk.Api.BusData.Resources;

/// <summary>
/// Provides access to the stops served by a bus trip.
/// </summary>
public sealed class TripStopsResource
{
    private readonly HttpClient _httpClient;

    public TripStopsResource(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Retrieves the stops for a bus trip, identified by timing point, scheduled departure time, or internal trip number.
    /// </summary>
    /// <param name="timingPointId">The timing point identifier to filter results by, or <c>null</c> to omit this filter.</param>
    /// <param name="schedDepTime">The scheduled departure time to filter results by, or <c>null</c> to omit this filter.</param>
    /// <param name="internalTripNumber">The internal trip number to filter results by, or <c>null</c> to omit this filter.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>The matching trip stops, or an empty list if none were returned.</returns>
    public async Task<IReadOnlyList<TripStopModel>?> GetTripStopsAsync(
        string? timingPointId = null,
        string? schedDepTime = null,
        string? internalTripNumber = null,
        CancellationToken cancellationToken = default)
    {
        var formDataContent = new MultipartFormDataContent();
        if (timingPointId is not null) formDataContent.Add(new StringContent(timingPointId), "timing_point_id");
        if (schedDepTime is not null) formDataContent.Add(new StringContent(schedDepTime), "sched_dep_time");
        if (internalTripNumber is not null) formDataContent.Add(new StringContent(internalTripNumber), "internal_trip_number");

        var response = await _httpClient.PostAsync("getTripStops", formDataContent, cancellationToken);
        var body = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Bus API returned {(int)response.StatusCode}: {body}");
        }

        var tripStops = await response.Content.ReadFromJsonAsync<TripStop[]>(cancellationToken: cancellationToken);
        return tripStops?.Select(stop => new TripStopModel
        {
            TripNumber = stop.TripNumber,
            TimePoint = stop.TimePoint,
            Description = stop.Description,
            SchedLaneGate = stop.SchedLaneGate,
            ManLaneGate = stop.ManLaneGate,
            SchedDepTime = stop.SchedDepTime,
            ApproxTime = stop.ApproxTime,
            StopId = stop.StopId,
            Status = stop.Status
        }).ToArray() ?? Array.Empty<TripStopModel>();
    }
}
