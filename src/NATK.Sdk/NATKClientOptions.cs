namespace NATK.Sdk

/// <summary>
/// Options for configuring the NATK client.
/// </summary>
{
    public class NATKClientOptions
    {
        
    /// <summary>API key used to authenticate requests.</summary>
    public required string ApiKey { get; set; }

    /// <summary>Base URL of the API. Defaults to the production endpoint.</summary>
    public Uri BaseUrl { get; set; }

    /// <summary>Maximum number of retry attempts for transient failures.</summary>
    public int MaxRetryAttempts { get; set; } = 3;
    }
}