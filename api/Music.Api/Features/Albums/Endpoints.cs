using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Music.Api.Common.Entities;
using Music.Api.Persistence;
using TrackEndpoints = Music.Api.Features.Tracks.Tracks;

namespace Music.Api.Features.Albums;

public static class Albums
{
    public static IEndpointRouteBuilder 
        MapAlbumsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", GetAllAlbums).WithName(nameof(GetAllAlbums));
        app.MapGet("/{id:guid}", GetAlbumById).WithName(nameof(GetAlbumById));
        app.MapPut("/{id:guid}", UpdateAlbum).WithName(nameof(UpdateAlbum));
        app.MapPost("/", AddAlbum).WithName(nameof(AddAlbum));
        app.MapDelete("/{id:guid}", RemoveAlbum).WithName(nameof(RemoveAlbum));
        
        app.MapPost("/{albumId:guid}/tracks", AddAlbumTrack).WithName(nameof(AddAlbumTrack));
        return app;
    }

    private static Task<List<Album>> GetAllAlbums(AppDbContext db) => db.Albums.ToListAsync();

    private static async Task<IResult> GetAlbumById(Guid id, AppDbContext db, CancellationToken ct)
    {
        var album = (await db.Albums
                .Include(x => x.Artist)
                .Include(x => x.Tracks)
                .FirstOrDefaultAsync(x => x.Id == id, ct)
            )?.ToAlbumResponse();
        
        return album is null
            ? Results.NotFound()
            : Results.Ok(album);
    }

    private static async Task<IResult> AddAlbumTrack(
        AppDbContext db,
        Guid albumId,
        AddTrackRequest request,
        LinkGenerator linkGenerator,
        HttpContext http,
        CancellationToken ct)
    {
        try
        {
            var (albumExists, tracksCount) = await CountAlbumTracks(albumId, db, ct);
            if (!albumExists)
            {
                return Results.NotFound("Album not found");
            }
            
            var track = new Track
            {
                Id = request.Id ?? Guid.NewGuid(),
                Title = request.Title,
                AlbumId = albumId,
                Duration = request.Duration,
                Position = tracksCount + 1
            };
            
            db.Tracks.Add(track);
            await db.SaveChangesAsync(ct);

            var uri = linkGenerator.GetUriByName(http, nameof(TrackEndpoints.GetTrackById), new { id = track.Id });
            return Results.Created(uri, track);
        }
        catch (Exception e)
        {
            return Results.Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }
    
    private static async Task<IResult> AddAlbum(
        AppDbContext db,
        AddAlbumRequest request,
        LinkGenerator linkGenerator,
        HttpContext http,
        CancellationToken ct)
    {
        var album = new Album
        {
            Id = request.Id ?? Guid.NewGuid(),
            ArtistId = request.ArtistId,
            Title = request.Title,
            Genre = request.Genre,
            Year = request.Year,
            CoverImageId = request.CoverImageId ?? Guid.Empty
        };
        
        try
        {
            db.Albums.Add(album);
            await db.SaveChangesAsync(ct);

            var uri = linkGenerator.GetUriByName(http, nameof(GetAlbumById), new { id = album.Id });
            return Results.Created(uri, album);
        }
        catch (Exception e)
        {
            return Results.Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    private static async Task<IResult> UpdateAlbum(
        AppDbContext db,
        Guid id,
        UpdateAlbumRequest request,
        LinkGenerator linkGenerator,
        HttpContext http,
        CancellationToken ct)
    {
        try
        {
            var album = await db.Albums.FindAsync([id], ct);
            if (album is null)
                return Results.NotFound();
            
            Hydrate(album, request);
            db.Albums.Update(album);
            await db.SaveChangesAsync(ct);

            return Results.Ok(album);
        }
        catch (Exception e)
        {
            return Results.Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }
    
    private static async Task<IResult> RemoveAlbum(
        Guid id,
        AppDbContext db,
        CancellationToken ct)
    {
        try
        {
            var album = await db.Albums.FindAsync([id], ct);
            if (album is null)
                return Results.NotFound();
            
            db.Albums.Remove(album);
            await db.SaveChangesAsync(ct);
            return Results.NoContent();
        }
        catch (Exception e)
        {
            return Results.Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    private static void Hydrate(Album album, UpdateAlbumRequest req)
    {
        album.ArtistId = req.ArtistId;
        album.Title = req.Title;
        album.Year = req.Year;
        album.Genre = req.Genre;
        album.CoverImageId = req.CoverImageId;
    }
    
    private static async Task<(bool albumExists, int tracksCount)> CountAlbumTracks(
        Guid albumId,
        AppDbContext db,
        CancellationToken ct)
    {
        var tracksCount = await db.Tracks.CountAsync(t => t.AlbumId == albumId, ct);
        var albumExists = 
            tracksCount > 0 ||  // If greater than 0, we know the album exists. No need to check
            await db.Albums.FindAsync([albumId], ct) is not null;   // otherwise we check
        
        return (albumExists, tracksCount);
    }
}