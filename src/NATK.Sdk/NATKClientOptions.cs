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
        public required string username { get; set; }

        /// <summary>
        /// Password for authenticating with the NJTrransit Developer Portal.
        /// </summary>
        public required string password { get; set; }

        /// <summary>
        /// The number of times to retry failed requests.
        /// </summary>
        public int retryCount { get; set; } = 3;
    }
}