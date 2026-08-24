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
    public Uri BusBaseUrl { get; set; } = new("https://pcsdata.njtransit.com/api/BUSDV2/");
    public Uri RailBaseUrl { get; set; } = new("https://api.example.com/v1/");

    /// <summary>Maximum number of retry attempts for transient failures.</summary>
    public int MaxRetryAttempts { get; set; } = 3;
    }
}