using System;
using System.Collections.Generic;

namespace Music.Api.Common.Entities;

public class Artist
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public Guid ImageId { get; set; } = Guid.NewGuid();
    /// <summary>
    /// Artist name
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Gets a brief description about the artist
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    public List<Album> Discography { get; set; }
}