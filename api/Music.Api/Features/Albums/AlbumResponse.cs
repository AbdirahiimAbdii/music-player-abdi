using System;
using System.Collections.Generic;

namespace Music.Api.Features.Albums;

public class AlbumResponse
{
    public Guid Id { get; set; }
    public AlbumArtistResponse Artist { get; set; }

    public Guid CoverImageId { get; set; } = Guid.Empty;
    
    public string Title { get; set; } = string.Empty;
    public int Year { get; set; } = DateTime.Now.Year;
    public string Genre { get; set; } = string.Empty;
    
    public List<TrackResponse> Tracks { get; set; } = new();
}

public class AlbumArtistResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid ImageId { get; set; }
}

public class TrackResponse
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid AlbumId { get; set; }

    public string Title { get; set; }

    public int Duration { get; set; }
}