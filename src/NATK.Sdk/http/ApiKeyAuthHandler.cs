using System.Net.Http.Headers;

namespace NATK.Sdk.Http;

internal sealed class ApiKeyAuthHandler : DelegatingHandler
{
    private readonly string _apiKey;

    public ApiKeyAuthHandler(string apiKey) => _apiKey = apiKey;

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Add the API key to the body of the request as a MultipartFormDataContent.
        if (request.Content is MultipartFormDataContent multipartContent)
        {
            multipartContent.Add(new StringContent(_apiKey), "token");
        }
        return base.SendAsync(request, cancellationToken);
    }
}
