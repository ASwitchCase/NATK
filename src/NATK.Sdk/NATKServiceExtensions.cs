using Microsoft.Extensions.DependencyInjection;
using NATK.Sdk.Http;
using NATK.Clients.Sdk;

namespace NATK.Sdk
{
    public static class NATKServiceExtensions
    {
        public static IServiceCollection AddNATKBusClient(this IServiceCollection services, Action<NATKClientOptions> configureOptions)
        {
            var options = new NATKClientOptions{ ApiKey = string.Empty };
            configureOptions(options);

            services.AddHttpClient<NATKBusClient>(client =>
            {
                client.BaseAddress = options.BaseUrl;

            }).AddHttpMessageHandler(() => new ApiKeyAuthHandler(options.ApiKey));
            return services;
        }
    }
}