using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Music.Api.Features.Tracks;

public static class Tracks
{
    public static IEndpointRouteBuilder MapTracksEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", GetAllTracks).WithName(nameof(GetAllTracks));
        app.MapGet("/{id:int}", GetTrackById).WithName(nameof(GetTrackById));
        app.MapPost("/", AddTrack).WithName(nameof(AddTrack));
        app.MapDelete("/{id:int}", RemoveTrack).WithName(nameof(RemoveTrack));
        return app;
    }

    private static string GetAllTracks()
    {
        return "fetch all tracks";
    }

    private static string GetTrackById(int id)
    {
        return $"fetch track with id = {id}";
    }

    private static string AddTrack()
    {
        return "add new track";
    }
    
    private static string RemoveTrack(int id)
    {
        return $"deleting track with id = {id}";
    }
}