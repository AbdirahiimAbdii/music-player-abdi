using System;

namespace Music.Api.Common.Entities;

public class AlbumTrack
{
    public int Position { get; set; }

    public Guid TrackId { get; set; }
    public Track Track { get; set; }
}