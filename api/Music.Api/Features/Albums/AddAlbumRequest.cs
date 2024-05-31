using System;
using System.ComponentModel.DataAnnotations;

namespace Music.Api.Features.Albums;

public class AddAlbumRequest
{
    public Guid? Id { get; set; }
    
    [Required]
    public Guid ArtistId { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public int Year { get; set; } = DateTime.Now.Year;

    public string Genre { get; set; } = string.Empty;
    
    public Guid? CoverImageId { get; set; } = null;
}