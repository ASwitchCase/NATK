using System.Net;
using Moq;
using Moq.Protected;

namespace NAKT.Sdk.Tests.TestHelpers;

internal static class HttpClientMockFactory
{
    public static HttpClient CreateClient(HttpStatusCode statusCode, string responseBody)
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(responseBody)
            });

        return new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("https://testraildata.njtransit.com/api/TrainData/")
        };
    }

    public static HttpClient CreateClient(HttpStatusCode statusCode, string responseBody, out Func<HttpRequestMessage?> capturedRequest)
    {
        HttpRequestMessage? captured = null;
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .Callback<HttpRequestMessage, CancellationToken>((request, _) => captured = request)
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(responseBody)
            });

        capturedRequest = () => captured;
        return new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("https://testraildata.njtransit.com/api/TrainData/")
        };
    }
}
