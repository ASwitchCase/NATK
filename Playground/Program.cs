using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using NATK.Sdk;
using DotNetEnv;
using NATK.Clients.Sdk;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddNATKBusClient(options =>
{
    options.ApiKey = Environment.GetEnvironmentVariable("NJTRANSIT_TOKEN")
        ?? throw new InvalidOperationException("NJTRANSIT_TOKEN is not set.");
});

builder.Services.AddNATKRailClient(options =>
{
    options.ApiKey = Environment.GetEnvironmentVariable("RAIL_TOKEN")
        ?? throw new InvalidOperationException("RAIL_TOKEN is not set.");
});

var app = builder.Build();

app.MapGet("/bus-locations", async (NATKBusClient natkClient) =>
{
    var busLocations = await natkClient.BusLocations.GetBusLocationsAsync();
    return Results.Ok(busLocations);
});

app.MapGet("/bus-routes", async (NATKBusClient natkClient, string mode = "ALL") =>
{
    var busRoutes = await natkClient.BusRoutes.GetBusRoutesAsync(mode);
    return Results.Ok(busRoutes);
});

app.MapGet("/bus-directions", async (NATKBusClient natkClient, string route) =>
{
    var busDirections = await natkClient.BusDirections.GetBusDirectionsAsync(route);
    return Results.Ok(busDirections);
});

app.MapGet("/bus-stops", async (NATKBusClient natkClient, string? route, string? direction, string? nameContains) =>
{
    var busStops = await natkClient.BusStops.GetBusStopsAsync(route, direction, nameContains);
    return Results.Ok(busStops);
});

app.MapGet("/route-trips", async (NATKBusClient natkClient, string? location, string? route) =>
{
    var routeTrips = await natkClient.RouteTrips.GetRouteTripsAsync(location, route);
    return Results.Ok(routeTrips);
});

app.MapGet("/stop-name", async (NATKBusClient natkClient, string stopNumber) =>
{
    var stopName = await natkClient.StopName.GetStopNameAsync(stopNumber);
    return Results.Ok(stopName);
});

app.MapGet("/bus-locations-data", async (NATKBusClient natkClient, string? route, string? direction, string? lat, string? lon, int? radius, string mode = "ALL") =>
{
    var busLocationsData = await natkClient.BusLocationsData.GetBusLocationsDataAsync(route, direction, lat, lon, radius, mode);
    return Results.Ok(busLocationsData);
});

app.MapGet("/bus-dv", async (NATKBusClient natkClient, string? stop, string? direction, string? route) =>
{
    var busDv = await natkClient.BusDV.GetBusDVAsync(stop, direction, route);
    return Results.Ok(busDv);
});

app.MapGet("/trip-stops", async (NATKBusClient natkClient, string? timingPointId, string? schedDepTime, string? internalTripNumber) =>
{
    var tripStops = await natkClient.TripStops.GetTripStopsAsync(timingPointId, schedDepTime, internalTripNumber);
    return Results.Ok(tripStops);
});

app.MapGet("/vehicle-locations", async (NATKBusClient natkClient, string? lat, string? lon, int radius = 1, string mode = "ALL") =>
{
    var vehicleLocations = await natkClient.VehicleLocations.GetVehicleLocationsAsync(lat, lon, radius, mode);
    return Results.Ok(vehicleLocations);
});

app.MapGet("/rail-stations", async (NATKRailClient natkClient) =>
{
    var trainStations = await natkClient.TrainStations.GetTrainStationsAsync();
    return Results.Ok(trainStations);
});

app.Run();