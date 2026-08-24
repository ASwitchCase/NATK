using System.Net.Http.Json;
using NATK.Sdk.Exceptions;
using NATK.Sdk.http.models;

namespace NATK.Sdk.Http;

/// <summary>
/// Fetches and caches an API token obtained by posting username/password as
/// form data to the token endpoint. Thread-safe: concurrent callers during a
/// refresh will wait for the single in-flight request rather than each firing
/// their own login call.
/// </summary>
internal sealed class TokenProvider
{
    private readonly HttpClient _authClient;
    private readonly NATKClientOptions _options;
    private readonly SemaphoreSlim _lock = new(1, 1);

    private string? _cachedToken;
    private DateTimeOffset _expiresAt = DateTimeOffset.MinValue;

    public TokenProvider(HttpClient authClient, NATKClientOptions options)
    {
        _authClient = authClient;
        _options = options;
    }

    /// <summary>
    /// Returns a valid token, fetching or refreshing one if needed.
    /// </summary>
    public async Task<string> GetTokenAsync(CancellationToken cancellationToken)
    {
        if (_cachedToken is not null && DateTimeOffset.UtcNow < _expiresAt)
        {
            return _cachedToken;
        }

        await _lock.WaitAsync(cancellationToken);
        try
        {
            if (_cachedToken is not null && DateTimeOffset.UtcNow < _expiresAt)
            {
                return _cachedToken;
            }

            return await FetchNewTokenAsync(cancellationToken);
        }
        finally
        {
            _lock.Release();
        }
    }

    /// <summary>
    /// Forces a refresh, ignoring any cached token. Useful when a request
    /// comes back 401 even though the cached token looked unexpired.
    /// </summary>
    public async Task<string> ForceRefreshAsync(CancellationToken cancellationToken)
    {
        await _lock.WaitAsync(cancellationToken);
        try
        {
            return await FetchNewTokenAsync(cancellationToken);
        }
        finally
        {
            _lock.Release();
        }
    }

    private async Task<string> FetchNewTokenAsync(CancellationToken cancellationToken)
    {
        var formData = new Dictionary<string, string>
        {
            ["username"] = _options.Username,
            ["password"] = _options.Password
        };

        using var content = new MultipartFormDataContent();
        foreach (var (key, value) in formData)
        {
            content.Add(new StringContent(value), key);
        }

        using var response = await _authClient.PostAsync(_options.TokenEndpoint, content, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new NATKAuthException(new Exception($"Token endpoint returned {response.StatusCode}: {body}"));
        }

        var tokenResponse = await response.Content.ReadFromJsonAsync<NJTransitToken>(cancellationToken: cancellationToken)
            ?? throw new NATKException("Token endpoint returned an empty response.");

        _cachedToken = tokenResponse.AccessToken;
        _expiresAt = DateTimeOffset.UtcNow
            .AddSeconds(tokenResponse.ExpiresIn)
            .Subtract(_options.TokenExpiryBuffer);

        return _cachedToken;
    }
}
