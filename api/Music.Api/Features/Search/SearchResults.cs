using System;
using System.Collections.Generic;

namespace Music.Api.Features.Search;

public class SearchResults
{
    public List<ArtistSearchResult> Artists { get; set; } = [];
    public List<AlbumSearchResult> Albums { get; set; } = [];
    public List<TrackSearchResult> Tracks { get; set; } = [];
}

public class ArtistSearchResult
{
    public Guid Id { get; set; }
    public Guid ImageId { get; set; }
    public string Name { get; set; }
}

public class AlbumSearchResult
{
    public Guid Id { get; set; }
    public Guid ImageId { get; set; }
    public string Title { get; set; }
}

public class TrackSearchResult
{
    public Guid Id { get; set; }
    public string Title { get; set; }
}