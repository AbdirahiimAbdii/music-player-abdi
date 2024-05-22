using System;

namespace Music.Api.Common.Entities;

public class Album
{
    public Guid Id { get; set; }

    public Guid ArtistId { get; set; }
    public Artist? Artist { get; set; }
    
    public string Title { get; set; } = string.Empty;
    public int Year { get; set; } = DateTime.Now.Year;
}