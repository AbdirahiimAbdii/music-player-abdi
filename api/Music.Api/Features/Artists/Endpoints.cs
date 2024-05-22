using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Music.Api.Features.Artists;

public static class Artists
{
    public static IEndpointRouteBuilder MapArtistsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", GetAllArtists).WithName(nameof(GetAllArtists));
        app.MapGet("/{id:int}", GetArtistById).WithName(nameof(GetArtistById));
        app.MapPost("/", AddArtist).WithName(nameof(AddArtist));
        app.MapDelete("/{id:int}", RemoveArtist).WithName(nameof(RemoveArtist));
        return app;
    }

    private static string GetAllArtists()
    {
        return "fetch all artists";
    }

    private static string GetArtistById(int id)
    {
        return $"fetch artist with id = {id}";
    }

    private static string AddArtist()
    {
        return "add new artist";
    }
    
    private static string RemoveArtist(int id)
    {
        return $"deleting artist with id = {id}";
    }
}