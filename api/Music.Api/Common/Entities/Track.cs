using System;

namespace Music.Api.Common.Entities;

public class Track
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid AlbumId { get; set; }
    public Album Album { get; set; }

    public string Title { get; set; }
    
    public int Position { get; set; }

    /// <summary>
    /// Gets the duration in seconds of this track
    /// </summary>
    public int Duration { get; set; }
}
