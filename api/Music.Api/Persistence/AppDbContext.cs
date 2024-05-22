using Microsoft.EntityFrameworkCore;
using Music.Api.Common.Entities;

namespace Music.Api.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Album> Albums { get; set; }
    public DbSet<AlbumTrack> AlbumTracks { get; set; }
    public DbSet<Image> Images { get; set; }
}