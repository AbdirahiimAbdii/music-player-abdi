using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Music.Api.Features.Albums;

public static class Albums
{
    public static IEndpointRouteBuilder MapAlbumsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", GetAllAlbums).WithName(nameof(GetAllAlbums));
        app.MapGet("/{id:int}", GetAlbumById).WithName(nameof(GetAlbumById));
        app.MapPost("/", AddAlbum).WithName(nameof(AddAlbum));
        app.MapDelete("/{id:int}", RemoveAlbum).WithName(nameof(RemoveAlbum));
        return app;
    }

    private static string GetAllAlbums()
    {
        return "fetch all albums";
    }

    private static string GetAlbumById(int id)
    {
        return $"fetch album with id = {id}";
    }

    private static string AddAlbum()
    {
        return "add new album";
    }
    
    private static string RemoveAlbum(int id)
    {
        return $"deleting album with id = {id}";
    }
}