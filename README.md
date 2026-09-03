# NATK SDK

NATK (NJTransit API Tool Kit) is a .NET SDK for experimenting with NJTransit
Bus and Rail APIs.

> **Disclaimer:** NATK is an independent, community project. It is not
> affiliated with, endorsed by, or sponsored by NJTransit. It is intended for
> testing, learning, and hobby projects. Review NJTransit's terms and API
> usage policies before using it.

## Requirements

- .NET 10 SDK
- An NJTransit developer account
- A temporary API token issued by NJTransit

## Set up NJTransit API access

1. Open the [NJTransit Developer Portal](https://developer.njtransit.com/).
2. Create a developer account, or sign in if you already have one.
3. Complete any account or email verification requested by the portal.
4. Open the API product or API console for the Bus or Rail API you want to
   test, then request or generate a temporary API token.
5. Copy the token value. Do not commit it to source control or place it in
   public client-side code. Create a new temporary token for each client or
   test application as required by NJTransit, and repeat the portal's token
   request process when the token expires or is revoked.

## Install and configure

```text
NJTRANSIT_TOKEN=replace-with-your-temporary-token
```

Configure the client with dependency injection:

```csharp
using NATK.Sdk;

builder.Services.AddNATKBusClient(options =>
{
    options.ApiKey = Environment.GetEnvironmentVariable("NJTRANSIT_TOKEN")
        ?? throw new InvalidOperationException("NJTRANSIT_TOKEN is not set.");
});
```

The production Bus API base URL is used by default. It can be overridden when
the API endpoint changes or a test endpoint is required:

```csharp
builder.Services.AddNATKBusClient(options =>
{
    options.ApiKey = token;
    options.BaseUrl = new Uri("https://pcsdata.njtransit.com/api/BUSDV2/");
});
```

## Example: bus locations

Inject `NATKBusClient` into an application service, endpoint, or controller:

```csharp
app.MapGet("/bus-locations", async (NATKBusClient client) =>
{
    var locations = await client.BusLocations.GetBusLocationsAsync();
    return Results.Ok(locations);
});
```

`GetBusLocationsAsync` requests all available locations (`mode=ALL`) and
returns an `IReadOnlyList<BusLocation>`.

## Example: rail data

Configure `NATKRailClient` the same way, then inject it into an application
service, endpoint, or controller:

```csharp
builder.Services.AddNATKRailClient(options =>
{
    options.ApiKey = Environment.GetEnvironmentVariable("RAIL_TOKEN")
        ?? throw new InvalidOperationException("RAIL_TOKEN is not set.");
});

app.MapGet("/rail-train-schedule", async (NATKRailClient client, string station) =>
{
    var schedule = await client.TrainSchedule.GetTrainScheduleAsync(station);
    return Results.Ok(schedule);
});
```

The default base URL points at NJ Transit's Rail test environment
(`https://testraildata.njtransit.com/api/TrainData/`) and can be overridden
via `options.BaseUrl`, the same way as the Bus client.

`NATKRailClient` exposes one resource per endpoint:

- `TrainStations` — all available train stations.
- `StationMessages` — station/line service messages, optionally filtered by
  station or line.
- `StationSchedule` — a station's daily schedule.
- `TrainSchedule` — real-time station schedules, via
  `GetTrainScheduleAsync` (full board) and `GetTrainScheduleByLineAsync`
  (up to 19 departures, optionally filtered by line).
- `TrainStopList` — the stop list and capacity data for a specific train.
- `VehicleData` — real-time GPS location and status for all trains.

Obtaining and validating an API token (`getToken`/`isValidToken`) is not
part of the SDK's resource surface and is left to the application.

## Run the sample

The `Playground` project is a minimal ASP.NET Core example. Set
`NJTRANSIT_TOKEN`, then run:

```bash
dotnet run --project Playground
```

Request the sample endpoint at `http://localhost:5000/bus-locations` or at the
URL printed by `dotnet run`.

## Build and test

```bash
dotnet build NATK.Sdk.slnx
dotnet test NATK.Sdk.slnx
```

## Project status

Bus location access and Rail station, schedule, stop list, and vehicle data
are available today. Additional API operations are still under development,
so public APIs may change while the project is being developed.