using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using NATK.Sdk;
using DotNetEnv;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddNATKBusClient(options =>
{
    options.ApiKey = Environment.GetEnvironmentVariable("NJTRANSIT_TOKEN")
        ?? throw new InvalidOperationException("NJTRANSIT_TOKEN is not set.");
});

var app = builder.Build();

app.MapGet("/bus-locations", async (NATKBusClient natkClient) =>
{
    var busLocations = await natkClient.BusLocations.GetBusLocationsAsync();
    return Results.Ok(busLocations);
});

app.Run();