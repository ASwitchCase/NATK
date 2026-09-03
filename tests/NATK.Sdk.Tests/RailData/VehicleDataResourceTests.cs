using System.Net;
using NATK.Sdk.Api.RailData.Resources;
using NAKT.Sdk.Tests.TestHelpers;

namespace NAKT.Sdk.Tests.RailData;

public class VehicleDataResourceTests
{
    [Fact]
    public async Task GetVehicleDataAsync_ReturnsMappedVehicles_OnSuccess()
    {
        var json = """
        [
            { "ID": "3901", "TRAIN_LINE": "NEC", "DIRECTION": "Southbound", "LATITUDE": "40.75", "LONGITUDE": "-74.00" }
        ]
        """;
        var httpClient = HttpClientMockFactory.CreateClient(HttpStatusCode.OK, json);
        var resource = new VehicleDataResource(httpClient);

        var result = await resource.GetVehicleDataAsync();

        Assert.NotNull(result);
        var vehicle = Assert.Single(result);
        Assert.Equal("3901", vehicle.Id);
        Assert.Equal("NEC", vehicle.TrainLine);
        Assert.Equal("40.75", vehicle.Latitude);
    }

    [Fact]
    public async Task GetVehicleDataAsync_Throws_OnFailureStatusCode()
    {
        var httpClient = HttpClientMockFactory.CreateClient(HttpStatusCode.ServiceUnavailable, "unavailable");
        var resource = new VehicleDataResource(httpClient);

        var exception = await Assert.ThrowsAsync<HttpRequestException>(() => resource.GetVehicleDataAsync());
        Assert.Contains("503", exception.Message);
    }
}
