using System;
using System.Collections.Generic;
using Music.Api.Common.Entities;

namespace Music.Api.Features.Artists;

public class AlbumResponse
{
    public Guid Id { get; set; }
    public Guid ArtistId { get; set; }

    public Guid CoverImageId { get; set; } = Guid.Empty;
    
    public string Title { get; set; } = string.Empty;
    public int Year { get; set; } = DateTime.Now.Year;
    public string Genre { get; set; } = string.Empty;
    
    public List<TrackResponse> Tracks { get; set; } = new();
}

public class TrackResponse
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid AlbumId { get; set; }

    public string Title { get; set; }
    
    public int Position { get; set; }

    public int Duration { get; set; }
}