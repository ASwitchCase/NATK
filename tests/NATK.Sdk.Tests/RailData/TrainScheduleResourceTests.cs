using System.Net;
using NATK.Sdk.Api.RailData.Resources;
using NAKT.Sdk.Tests.TestHelpers;

namespace NAKT.Sdk.Tests.RailData;

public class TrainScheduleResourceTests
{
    private const string NestedStationInfoJson = """
    {
        "STATION_2CHAR": "NY",
        "STATIONNAME": "New York Penn Station",
        "STATIONMSGS": [
            { "MSG_TYPE": "ALERT", "MSG_TEXT": "Delays reported" }
        ],
        "ITEMS": [
            {
                "SCHED_DEP_DATE": "2026-08-27 10:00:00",
                "DESTINATION": "Trenton",
                "TRACK": "5",
                "LINE": "NEC",
                "TRAIN_ID": "3901",
                "STATUS": "ON TIME",
                "CAPACITY": [
                    {
                        "VEHICLE_NO": "7001",
                        "SECTIONS": [
                            {
                                "SECTION_POSITION": "1",
                                "CARS": [
                                    { "CAR_NO": "7001", "CAR_POSITION": "1", "CAR_REST": true, "CUR_PERCENTAGE": "50" }
                                ]
                            }
                        ]
                    }
                ],
                "STOPS": [
                    {
                        "STATION_2CHAR": "NY",
                        "STATIONNAME": "New York Penn Station",
                        "STOP_LINES": [
                            { "LINE_CODE": "NEC", "LINE_NAME": "Northeast Corridor", "LINE_COLOR": "#FF0000" }
                        ]
                    }
                ]
            }
        ]
    }
    """;

    [Fact]
    public async Task GetTrainScheduleAsync_MapsNestedCapacityAndStops_OnSuccess()
    {
        var httpClient = HttpClientMockFactory.CreateClient(HttpStatusCode.OK, NestedStationInfoJson);
        var resource = new TrainScheduleResource(httpClient);

        var result = await resource.GetTrainScheduleAsync("NY");

        Assert.NotNull(result);
        Assert.Equal("NY", result!.Station2Char);
        var message = Assert.Single(result.StationMsgs);
        Assert.Equal("ALERT", message.MsgType);

        var item = Assert.Single(result.Items);
        Assert.Equal("3901", item.TrainId);

        var capacity = Assert.Single(item.Capacity);
        Assert.Equal("7001", capacity.VehicleNo);
        var section = Assert.Single(capacity.Sections);
        var car = Assert.Single(section.Cars);
        Assert.True(car.CarRest);
        Assert.Equal("50", car.CurPercentage);

        var stop = Assert.Single(item.Stops);
        var line = Assert.Single(stop.StopLines);
        Assert.Equal("NEC", line.LineCode);
    }

    [Fact]
    public async Task GetTrainScheduleAsync_ReturnsNull_WhenBodyIsNull()
    {
        var httpClient = HttpClientMockFactory.CreateClient(HttpStatusCode.OK, "null");
        var resource = new TrainScheduleResource(httpClient);

        var result = await resource.GetTrainScheduleAsync("NY");

        Assert.Null(result);
    }

    [Fact]
    public async Task GetTrainScheduleAsync_Throws_OnFailureStatusCode()
    {
        var httpClient = HttpClientMockFactory.CreateClient(HttpStatusCode.NotFound, "not found");
        var resource = new TrainScheduleResource(httpClient);

        var exception = await Assert.ThrowsAsync<HttpRequestException>(() => resource.GetTrainScheduleAsync("NY"));
        Assert.Contains("404", exception.Message);
    }

    [Fact]
    public async Task GetTrainScheduleByLineAsync_PostsToLineFilteredEndpoint_AndOmitsLineWhenNull()
    {
        var httpClient = HttpClientMockFactory.CreateClient(HttpStatusCode.OK, NestedStationInfoJson, out var capturedRequest);
        var resource = new TrainScheduleResource(httpClient);

        await resource.GetTrainScheduleByLineAsync("NY");

        var request = capturedRequest();
        Assert.NotNull(request);
        Assert.EndsWith("getTrainSchedule19Rec", request!.RequestUri!.ToString());
        var content = await request.Content!.ReadAsStringAsync();
        Assert.DoesNotContain("name=line", content);
    }

    [Fact]
    public async Task GetTrainScheduleByLineAsync_IncludesLine_WhenProvided()
    {
        var httpClient = HttpClientMockFactory.CreateClient(HttpStatusCode.OK, NestedStationInfoJson, out var capturedRequest);
        var resource = new TrainScheduleResource(httpClient);

        await resource.GetTrainScheduleByLineAsync("NY", "NEC");

        var request = capturedRequest();
        var content = await request!.Content!.ReadAsStringAsync();
        Assert.Contains("name=line", content);
        Assert.Contains("NEC", content);
    }
}
