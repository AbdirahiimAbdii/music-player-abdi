using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Music.Api.Persistence;

namespace Music.Api.Features.Search;

public static class SearchEndpoints
{
    public static IEndpointRouteBuilder MapSearchEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", Search)
            .WithName(nameof(Search))
            .Produces(StatusCodes.Status200OK);
        
        return app;
    }
    
    private static async Task<IResult> Search(
        AppDbContext db,
        [FromQuery] string q,
        [FromQuery] int? limit,
        CancellationToken ct
        )
    {
        var limitOrDefault = limit ?? 10;
        var searchPattern = $"%{q}%";
        
        var artists = await db.Artists
            .Where(x => 
                EF.Functions.Like(x.Name, searchPattern) ||
                EF.Functions.Like(x.Name, searchPattern))
            .Take(limitOrDefault)
            .Select(x => new ArtistSearchResult { Id = x.Id, Name = x.Name, ImageId = x.ImageId})
            .ToListAsync(ct);

        var albums = await db.Albums
            .Where(x => 
                EF.Functions.Like(x.Title, searchPattern) ||
                EF.Functions.Like(x.Genre, searchPattern))
            .Take(limitOrDefault)
            .Select(x => new AlbumSearchResult { Id = x.Id, Title = x.Title, ImageId = x.CoverImageId})
            
            .ToListAsync(ct);

        var tracks = await db.Tracks
            .Where(x => EF.Functions.Like(x.Title, searchPattern))
            .Take(limitOrDefault)
            .Select(x => new TrackSearchResult { Id = x.Id, Title = x.Title })
            .ToListAsync(ct);

        return Results.Ok(new SearchResults
        {
            Artists = artists,
            Albums = albums,
            Tracks = tracks
        });
    }
}