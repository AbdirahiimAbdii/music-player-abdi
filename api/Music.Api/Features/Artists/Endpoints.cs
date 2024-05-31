using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Music.Api.Common.Entities;
using Music.Api.Features.Albums;
using Music.Api.Features.Images;
using Music.Api.Persistence;

namespace Music.Api.Features.Artists;

public static class Artists
{
    // Create a regex object
    private static readonly Regex Base64Data = new(@"data:image\/(?<imgType>[^;]+);base64,(?<data>.+)");
    
    public static IEndpointRouteBuilder MapArtistsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", GetAllArtists).WithName(nameof(GetAllArtists));
        app.MapGet("/{id:guid}", GetArtistById).WithName(nameof(GetArtistById));
        app.MapPost("/", AddArtist).WithName(nameof(AddArtist));
        app.MapDelete("/{id:guid}", RemoveArtist).WithName(nameof(RemoveArtist));
        app.MapGet("/{id:guid}/discography", GetArtistDiscography).WithName(nameof(GetArtistDiscography));
        app.MapPost("/{id:guid}/discography", AddAlbumToDiscography).WithName(nameof(AddAlbumToDiscography));
        return app;
    }

    private static Task<List<Artist>> GetAllArtists(AppDbContext db, CancellationToken ct) => db.Artists.ToListAsync(ct);

    private static async Task<IResult> GetArtistById(Guid id, AppDbContext db, CancellationToken ct)
    {
        var artist = await db.Artists
            .Include(a => a.Discography)
            // .ThenInclude(a => a.Tracks)
            .FirstOrDefaultAsync(x => x.Id == id, ct);
        
        return artist is null
            ? Results.NotFound()
            : Results.Ok(artist.ToArtistResponse());
    }

    private static async Task<IResult> AddArtist(
        AppDbContext db,
        AddArtistRequest request,
        LinkGenerator linkGenerator,
        HttpContext http,
        CancellationToken ct)
    {
        try
        {
            var img = await GetOrCreateImageAsync(request.Image, request.Name, db, ct);
            if (img is null)
                return Results.NotFound($"Image with id ${request.Image} not found");

            db.Images.Add(img);
            
            var artist = new Artist
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                ImageId = img.Id,
                Description = request.Description
            };

            db.Artists.Add(artist);
            await db.SaveChangesAsync(ct);

            var uri = linkGenerator.GetUriByName(http, nameof(GetArtistById), new { id = artist.Id });
            return Results.Created(uri, artist);
        }
        catch (ArgumentOutOfRangeException e)
        {
            return Results.Problem(e.Message, statusCode: StatusCodes.Status400BadRequest);
        }
        catch (Exception e)
        {
            return Results.Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    private static async Task<IResult> GetArtistDiscography(Guid id, AppDbContext db, CancellationToken ct)
    {
        var artist = await db.Artists.FindAsync([id], ct);
        if (artist is null)
        {
            return Results.NotFound();
        }

        var albums = await db.Albums
            .Include(a => a.Tracks)
            .Where(album => album.ArtistId == id)
            .Select(album => album.ToAlbumResponse())
            .ToListAsync(ct);

        return Results.Ok(albums);
    }
    
    private static async Task<IResult> AddAlbumToDiscography(Guid id, AddArtistAlbumRequest request, AppDbContext db, CancellationToken ct)
    {
        var artist = await db.Artists.FindAsync([id], ct);
        if (artist is null)
        {
            return Results.NotFound();
        }

        try
        {
            var albumDescription = CreateAlbumImageDescription(request.Title, artist.Name);
            var coverImage = await GetOrCreateImageAsync(request.CoverImage, albumDescription, db, ct);
            if (coverImage is null)
                return Results.NotFound($"Image with id ${request.CoverImage} not found");

            db.Images.Add(coverImage);
            
            var album = new Album
            {
                Id = Guid.NewGuid(),
                ArtistId = artist.Id,
                Title = request.Title,
                Genre = request.Genre,
                Year = request.Year,
                CoverImageId = coverImage.Id
            };
            db.Albums.Add(album);
            await db.SaveChangesAsync(ct);
            return Results.Created(string.Empty, album.ToAlbumResponse());
        }
        catch (ArgumentOutOfRangeException e)
        {
            return Results.Problem(e.Message, statusCode: StatusCodes.Status400BadRequest);
        }
        catch (Exception e)
        {
            return Results.Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }
    
    private static async Task<IResult> RemoveArtist(Guid id, AppDbContext db, CancellationToken ct)
    {
        var artist = await db.Artists.FindAsync([id], ct);
        
        if (artist is null)
            return Results.NotFound();

        try
        {
            db.Artists.Remove(artist);
            await db.SaveChangesAsync(ct);
            return Results.NoContent();
        }
        catch(Exception e)
        {
            return Results.Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    private static async Task<Image?> GetOrCreateImageAsync(string image, string imageDescription, AppDbContext db, CancellationToken ct)
    {
        if (Guid.TryParse(image, out var id))
            return await db.Images.FindAsync([id], ct);
        
        var match = Base64Data.Match(image);

        if (!match.Success)
            throw new ArgumentOutOfRangeException($"Invalid image format");

        var imgType = match.Groups["imgType"].Value;
        var base64Data = match.Groups["data"].Value;

        if (!Enum.TryParse<ImageType>(imgType, true, out var _))
            throw new ArgumentOutOfRangeException(nameof(image), $"Unsupported image type ${imgType}");
        
        return new Image
        {
            Id = Guid.NewGuid(),
            Description = imageDescription,
            Content = Convert.FromBase64String(base64Data),
            Mime = $"image/${imgType}"
        };
    }

    private static string ToSnakeCase(string name)
    {
        return string.Join('_', name.ToLower().Split(' ',
            StringSplitOptions.RemoveEmptyEntries |
            StringSplitOptions.TrimEntries));
    }
    private static string CreateAlbumImageDescription(string albumTitle, string artistName)
    {
        return $"{ToSnakeCase(albumTitle)}.{ToSnakeCase(artistName)}";
    }
}