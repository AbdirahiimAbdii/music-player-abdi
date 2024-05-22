using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Music.Api.Features.Albums;
using Music.Api.Features.Artists;
using Music.Api.Features.Images;
using Music.Api.Features.Tracks;
using Music.Api.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure persistence
builder.Services.AddDbContext<AppDbContext>(
    option => option.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Register endpoints
app.MapGroup("api/v1/albums").WithTags("Albums").MapAlbumsEndpoints();
app.MapGroup("api/v1/artists").WithTags("Artists").MapArtistsEndpoints();
app.MapGroup("api/v1/images").WithTags("Images").MapImagesEndpoints();
app.MapGroup("api/v1/tracks").WithTags("Tracks").MapTracksEndpoints();

app.Run();
