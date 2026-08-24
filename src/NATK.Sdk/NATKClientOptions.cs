namespace NATK.Sdk

/// <summary>
/// Options for configuring the NATK client.
/// </summary>
{
    public class NATKClientOptions
    {
        /// <summary>
        /// Username for authenticating with the NJTrransit Developer Portal.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password for authenticating with the NJTrransit Developer Portal.
        /// </summary>
        public string Password { get; set; }

        /// <summary>Relative path (from <see cref="BaseUrl"/>) of the token endpoint.</summary>
        public string TokenEndpoint { get; set; } = "pcsdata.njtransit.com/api/BUSDV2/authenticateUser";

        /// <summary>
        /// How much earlier than the token's real expiry to treat it as expired,
        /// giving in-flight requests a safety margin. Defaults to 30 seconds.
        /// </summary>
        public TimeSpan TokenExpiryBuffer { get; set; } = TimeSpan.FromSeconds(86400);

        /// <summary>Maximum number of retry attempts for transient failures.</summary>
        public int MaxRetryAttempts { get; set; } = 3;
    }
}