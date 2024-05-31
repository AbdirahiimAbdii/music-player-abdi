using System;
using System.ComponentModel.DataAnnotations;

namespace Music.Api.Features.Albums;

public class UpdateAlbumRequest
{
    [Required]
    public Guid ArtistId { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public int Year { get; set; }
    
    [Required]
    public string Genre { get; set; }

    [Required]
    public Guid CoverImageId { get; set; }
}