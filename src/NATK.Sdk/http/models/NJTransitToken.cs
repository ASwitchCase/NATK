using System.Text.Json.Serialization;
namespace NATK.Sdk.http.models;

public class NJTransitToken
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = string.Empty;

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }

}