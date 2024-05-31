using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace Music.Api.Persistence;

public class GuidToStringConverter : ValueConverter<Guid, string>
{
    public GuidToStringConverter()
        : base(
            guid => guid.ToString(),
            str => new Guid(str))
    { }
}