using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Music.Api.Common.Entities;
using Music.Api.Persistence;

namespace Music.Api.Features.Tracks;

public static class Tracks
{
    public static IEndpointRouteBuilder MapTracksEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", GetAllTracks).WithName(nameof(GetAllTracks));
        app.MapGet("/{id:guid}", GetTrackById).WithName(nameof(GetTrackById));
        app.MapPut("/{id:guid}", UpdateTrack).WithName(nameof(UpdateTrack));
        app.MapDelete("/{id:guid}", RemoveTrack).WithName(nameof(RemoveTrack));
        return app;
    }
    
    // This endpoint can be used in future releases to allow track search
    // At the moment it won't be used
    private static Task<List<Track>> GetAllTracks(AppDbContext db, CancellationToken ct)
        => db.Tracks.ToListAsync(ct);

    internal static async Task<IResult> GetTrackById(Guid id, AppDbContext db, CancellationToken ct)
    {
        var track = await db.Tracks.FindAsync([id], ct);
        return track is null
            ? Results.NotFound()
            : Results.Ok(track);
    }

    private static async Task<IResult> UpdateTrack(
        AppDbContext db,
        Guid id,
        UpdateTrackRequest request,
        LinkGenerator linkGenerator,
        HttpContext http,
        CancellationToken ct)
    {
        try
        {
            var track = await db.Tracks.FindAsync([id], ct);
            if (track is null)
                return Results.NotFound();
            
            track.Title = request.Title;
            track.Duration = request.Duration;
            
            db.Tracks.Update(track);
            await db.SaveChangesAsync(ct);

            return Results.Ok(track);
        }
        catch (Exception e)
        {
            return Results.Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }
    
    private static async Task<IResult> RemoveTrack(
        Guid id,
        AppDbContext db,
        CancellationToken ct
        )
    {
        await using var transaction = await db.Database.BeginTransactionAsync(ct);

        try
        {
            var track = await db.Tracks.FindAsync([id], ct);

            if (track is null)
                return Results.NotFound("Track not found");

            var affectedTracks = db.Tracks
                .Where(t => t.AlbumId == track.AlbumId && t.Position > track.Position);

            foreach (var affectedTrack in affectedTracks)
                affectedTrack.Position--;

            db.Tracks.Remove(track);
            await db.SaveChangesAsync(ct);
            await transaction.CommitAsync(ct);
            return Results.NoContent();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(ct);
            return Results.Problem(e.Message);
        }
    }
}