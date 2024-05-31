using System;

namespace Music.Api.Features.Images;

public class AddImageRequest
{
    public Guid? Id { get; set; }
    public string Description { get; set; } = string.Empty;
    /// <summary>
    /// Image type
    /// </summary>
    public string Type { get; set; }
    /// <summary>
    /// Base64 encoded content
    /// </summary>
    public string Content { get; set; }
}
