using System.Net;
using NATK.Sdk.Api.RailData.Resources;
using NAKT.Sdk.Tests.TestHelpers;

namespace NAKT.Sdk.Tests.RailData;

public class TrainStationResourceTests
{
    [Fact]
    public async Task GetTrainStationsAsync_ReturnsMappedStations_OnSuccess()
    {
        var json = """
        [
            { "STATION_2CHAR": "NY", "STATIONNAME": "New York Penn Station", "STATION_14CHAR": "NEW YORK", "WHEELCHAIR_ACCESSIBLE": "Y" }
        ]
        """;
        var httpClient = HttpClientMockFactory.CreateClient(HttpStatusCode.OK, json);
        var resource = new TrainStationResource(httpClient);

        var result = await resource.GetTrainStationsAsync();

        Assert.NotNull(result);
        var station = Assert.Single(result);
        Assert.Equal("NY", station.Station2Char);
        Assert.Equal("New York Penn Station", station.StationName);
        Assert.Equal("NEW YORK", station.Station14Char);
        Assert.Equal("Y", station.WheelchairAccessible);
    }

    [Fact]
    public async Task GetTrainStationsAsync_Throws_OnFailureStatusCode()
    {
        var httpClient = HttpClientMockFactory.CreateClient(HttpStatusCode.InternalServerError, "server error");
        var resource = new TrainStationResource(httpClient);

        var exception = await Assert.ThrowsAsync<HttpRequestException>(() => resource.GetTrainStationsAsync());
        Assert.Contains("500", exception.Message);
    }
}
