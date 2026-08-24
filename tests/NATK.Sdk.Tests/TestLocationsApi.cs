using System.Net;
using Moq;
using Moq.Protected;
using NATK.Sdk;

public class TestLocationsApi
{
    [Fact]
    public async Task GetBusLocationsAsync_ReturnsListOfBusLocations()
    {
        // Arrange
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("[{\"BusTerminalCode\":\"123\",\"BusTerminalName\":\"Test Terminal\"}]"),
            });

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        var clientOptions = new NATKClientOptions { ApiKey = "test_api_key" };
        var client = new NATKBusClient(clientOptions);

        // Act
        var busLocations = await client.BusLocations.GetBusLocationsAsync();

        // Assert
        Assert.NotNull(busLocations);
        Assert.Single(busLocations);
        Assert.Equal("123", busLocations[0].BusTerminalCode);
        Assert.Equal("Test Terminal", busLocations[0].BusTerminalName);
    }
}