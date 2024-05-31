using System;
using System.Collections.Generic;

namespace Music.Api.Common.Entities;

public class Album
{
    public Guid Id { get; set; }

    public Guid ArtistId { get; set; }
    public Artist? Artist { get; set; }

    public Guid CoverImageId { get; set; } = Guid.Empty;
    
    public string Title { get; set; } = string.Empty;
    public int Year { get; set; } = DateTime.Now.Year;
    public string Genre { get; set; } = string.Empty;
    
    public List<Track> Tracks { get; set; } = new();
}