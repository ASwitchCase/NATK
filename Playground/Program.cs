using NATK.Sdk;
using DotNetEnv;

Env.Load();

var client = new NATKClient(new NATKClientOptions
{
    ApiKey = Environment.GetEnvironmentVariable("NJTRANSIT_TOKEN") ?? throw new InvalidOperationException("NJTRANSIT_TOKEN environment variable is not set."),
});

var busLocations = await client.BusLocations.GetBusLocationsAsync();
Console.WriteLine($"Retrieved {busLocations.Count} bus locations.");
Console.WriteLine("Bus Locations:");
foreach (var location in busLocations)
{
    Console.WriteLine($"Code: {location.BusTerminalCode}, Name: {location.BusTerminalName}");
}