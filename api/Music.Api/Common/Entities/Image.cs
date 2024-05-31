using System;

namespace Music.Api.Common.Entities;

public class Image
{
    public Guid Id { get; set; }
    public string Mime { get; set; }
    
    public string Description { get; set; } = string.Empty;
    public byte[] Content { get; set; } = [];
}