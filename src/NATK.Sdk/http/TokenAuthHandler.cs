using System.Net;
using System.Net.Http.Headers;
using NATK.Sdk.Http;

namespace MyCompany.Sdk.Http;

/// <summary>
/// Attaches a bearer token (obtained via username/password login) to every
/// outgoing request. If a request comes back 401, forces one token refresh
/// and retries the request once.
/// </summary>
internal sealed class TokenAuthHandler : DelegatingHandler
{
    private readonly TokenProvider _tokenProvider;

    public TokenAuthHandler(TokenProvider tokenProvider)
    {
        _tokenProvider = tokenProvider;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _tokenProvider.GetTokenAsync(cancellationToken);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            response.Dispose();

            var freshToken = await _tokenProvider.ForceRefreshAsync(cancellationToken);

            var retryRequest = await CloneRequestAsync(request);
            retryRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", freshToken);

            response = await base.SendAsync(retryRequest, cancellationToken);
        }

        return response;
    }

    private static async Task<HttpRequestMessage> CloneRequestAsync(HttpRequestMessage original)
    {
        var clone = new HttpRequestMessage(original.Method, original.RequestUri);

        if (original.Content is not null)
        {
            var bytes = await original.Content.ReadAsByteArrayAsync();
            clone.Content = new ByteArrayContent(bytes);

            foreach (var header in original.Content.Headers)
            {
                clone.Content.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
        }

        foreach (var header in original.Headers)
        {
            clone.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        return clone;
    }
}
