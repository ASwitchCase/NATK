using System.Net;
using NATK.Sdk.Api.RailData.Resources;
using NAKT.Sdk.Tests.TestHelpers;

namespace NAKT.Sdk.Tests.RailData;

public class StationMessagesResourceTests
{
    [Fact]
    public async Task GetStationMessagesAsync_ReturnsMappedMessages_OnSuccess()
    {
        var json = """
        [
            { "MSG_TYPE": "ALERT", "MSG_TEXT": "Delays reported", "MSG_ID": "123" }
        ]
        """;
        var httpClient = HttpClientMockFactory.CreateClient(HttpStatusCode.OK, json);
        var resource = new StationMessagesResource(httpClient);

        var result = await resource.GetStationMessagesAsync();

        Assert.NotNull(result);
        var message = Assert.Single(result);
        Assert.Equal("ALERT", message.MsgType);
        Assert.Equal("Delays reported", message.MsgText);
        Assert.Equal("123", message.MsgId);
    }

    [Fact]
    public async Task GetStationMessagesAsync_SendsStationAndLineFormFields()
    {
        var httpClient = HttpClientMockFactory.CreateClient(HttpStatusCode.OK, "[]", out var capturedRequest);
        var resource = new StationMessagesResource(httpClient);

        await resource.GetStationMessagesAsync("NY", "NEC");

        var request = capturedRequest();
        Assert.NotNull(request);
        Assert.EndsWith("getStationMSG", request!.RequestUri!.ToString());
        var content = await request.Content!.ReadAsStringAsync();
        Assert.Contains("NY", content);
        Assert.Contains("NEC", content);
    }

    [Fact]
    public async Task GetStationMessagesAsync_Throws_OnFailureStatusCode()
    {
        var httpClient = HttpClientMockFactory.CreateClient(HttpStatusCode.BadRequest, "bad request");
        var resource = new StationMessagesResource(httpClient);

        var exception = await Assert.ThrowsAsync<HttpRequestException>(() => resource.GetStationMessagesAsync());
        Assert.Contains("400", exception.Message);
    }
}
