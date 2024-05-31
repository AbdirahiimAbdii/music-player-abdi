using System;
using System.Linq;
using Music.Api.Common.Entities;

namespace Music.Api.Features.Albums;

public static class Mappers
{
    public static AlbumResponse ToAlbumResponse(this Album album)
        => new()
        {
            Id = album.Id,
            Artist = album.Artist?.ToTrackAlbumArtistResponse() 
                     ?? throw new NullReferenceException("Undefined artist"),
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
            Title = track.Title
        };
    
    public static AlbumArtistResponse ToTrackAlbumArtistResponse(this Artist artist)
        => new()
        {
            Id = artist.Id,
            Name = artist.Name,
            ImageId = artist.ImageId
        };
}