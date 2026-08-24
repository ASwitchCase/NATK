using NATK.Sdk;

var client = new NATKClient(new NATKClientOptions
{
    ApiKey = "639231793266433934"
});

var busLocations = await client.BusLocations.GetBusLocationsAsync();
Console.WriteLine($"Retrieved {busLocations.Count} bus locations.");
Console.WriteLine("Bus Locations:");
foreach (var location in busLocations)
{
    Console.WriteLine($"Code: {location.BusTerminalCode}, Name: {location.BusTerminalName}");
}