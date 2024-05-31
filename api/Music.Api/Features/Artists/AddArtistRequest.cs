using System;
using System.ComponentModel.DataAnnotations;

namespace Music.Api.Features.Artists;

public class AddArtistRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    public string Image { get; set; } = string.Empty;
}