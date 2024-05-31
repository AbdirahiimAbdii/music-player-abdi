using System;
using System.Collections.Generic;

namespace Music.Api.Features.Artists;

public class ArtistResponse
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public Guid ImageId { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    
    public List<AlbumResponse> Discography { get; set; }
}
