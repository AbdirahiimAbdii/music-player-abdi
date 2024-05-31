using System;
using System.ComponentModel.DataAnnotations;

namespace Music.Api.Features.Artists;

public class AddArtistAlbumRequest
{
    [Required]
    public string Title { get; set; } = string.Empty;

    public int Year { get; set; } = DateTime.Now.Year;

    public string Genre { get; set; } = string.Empty;
    
    public string CoverImage { get; set; } = null;
}