namespace NATK.Sdk.http.models;

/// <summary>
/// Represents the response from the API token authentication endpoint.
/// </summary>
public class GetTokenResponse
{
    public bool Authenticated { get; set; }
    public string? UserToken { get; set; }
}