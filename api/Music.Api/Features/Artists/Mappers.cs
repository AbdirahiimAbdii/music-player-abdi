using System.Linq;
using Music.Api.Common.Entities;

namespace Music.Api.Features.Artists;

public static class Mappers
{
    public static ArtistResponse ToArtistResponse(this Artist artist)
        => new()
        {
            Id = artist.Id,
            ImageId = artist.ImageId,
            Name = artist.Name,
            Description = artist.Description,
            Discography = artist.Discography.Select(album => album.ToAlbumResponse()).ToList() 
        };

    public static AlbumResponse ToAlbumResponse(this Album album)
        => new()
        {
            Id = album.Id,
            ArtistId = album.ArtistId,
            CoverImageId = album.CoverImageId,
            Title = album.Title,
            Year = album.Year,
            Genre = album.Genre,
            Tracks = album.Tracks
                .OrderBy(x => x.Position)
                .Select(x=>x.ToTrackResponse())
                .ToList()
        };
    
    public static TrackResponse ToTrackResponse(this Track track)
        => new()
        {
            Id = track.Id,
            AlbumId = track.AlbumId,
            Duration = track.Duration,
            Position = track.Position,
            Title = track.Title,
        };
}