# NATK SDK

NATK (NJTransit API Tool Kit) is a .NET SDK for experimenting with NJTransit
Bus and Rail APIs. The project is currently focused on the Bus API, including
retrieving bus locations. Rail API support is planned as the SDK grows.

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

The portal is the source of truth for current account approval, token lifetime,
rate limits, and endpoint access. NATK does not currently create, refresh,
cache, or revoke tokens. It passes the token supplied in `ApiKey` to the API as
the multipart form field named `token`.

## Install and configure

From a .NET application, add a project or package reference to NATK. When using
the project in this repository:

```xml
<ProjectReference Include="../src/NATK.Sdk/NATK.Sdk.csproj" />
```

Store the temporary token in an environment variable. For local development,
you can use a `.env` file that is ignored by git:

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

Bus location access is available today. Rail resources and additional API
operations are still under development, so public APIs may change while the
project is being developed.