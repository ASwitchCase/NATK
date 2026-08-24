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
                client.BaseAddress = options.BaseUrl ?? new("https://pcsdata.njtransit.com/api/BUSDV2/");

            }).AddHttpMessageHandler(() => new ApiKeyAuthHandler(options.ApiKey));
            return services;
        }
        public static IServiceCollection AddNATKRailClient(this IServiceCollection services, Action<NATKClientOptions> configureOptions)
        {
            var options = new NATKClientOptions{ ApiKey = string.Empty };
            configureOptions(options);

            services.AddHttpClient<NATKRailClient>(client =>
            {
                client.BaseAddress = options.BaseUrl ?? new("https://testraildata.njtransit.com/api/TrainData/");

            }).AddHttpMessageHandler(() => new ApiKeyAuthHandler(options.ApiKey));
            return services;
        }
    }
}