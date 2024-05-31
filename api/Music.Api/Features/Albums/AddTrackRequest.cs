using System;
using System.ComponentModel.DataAnnotations;

namespace Music.Api.Features.Albums;

public class AddTrackRequest
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }
    
    [Range(0, int.MaxValue, ErrorMessage = "Duration must be non negative")]
    public int Duration { get; set; }
}