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

app.MapGet("/rail-stations", async (NATKRailClient natkClient) =>
{
    var trainStations = await natkClient.TrainStations.GetTrainStationsAsync();
    return Results.Ok(trainStations);
});

app.Run();