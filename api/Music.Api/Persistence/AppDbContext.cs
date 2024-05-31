using Microsoft.EntityFrameworkCore;
using Music.Api.Common.Entities;

namespace Music.Api.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Album> Albums { get; set; }
    public DbSet<Track> Tracks { get; set; }
    public DbSet<Image> Images { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Artist>()
            .HasMany(u => u.Discography)
            .WithOne(album => album.Artist)
            .HasForeignKey(album => album.ArtistId);
        
        modelBuilder.Entity<Album>()
            .HasMany(u => u.Tracks)
            .WithOne(c => c.Album)
            .HasForeignKey(c => c.AlbumId);
    }
}