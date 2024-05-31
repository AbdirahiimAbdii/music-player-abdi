using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Music.Api.Features.Albums;
using Music.Api.Features.Artists;
using Music.Api.Features.Images;
using Music.Api.Features.Search;
using Music.Api.Features.Tracks;
using Music.Api.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure persistence
SQLitePCL.Batteries_V2.Init();
builder.Services.AddDbContext<AppDbContext>(
    option => option.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("_local", policy => policy
        .WithOrigins("http://localhost:3000", "https://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod()
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsEnvironment("Test"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
    Console.WriteLine("Testing testing!");
}

app.UseCors("_local");
app.UseHttpsRedirection();

// Register endpoints
app.MapGroup("api/v1/albums").WithTags("Albums").MapAlbumsEndpoints();
app.MapGroup("api/v1/artists").WithTags("Artists").MapArtistsEndpoints();
app.MapGroup("api/v1/images").WithTags("Images").MapImagesEndpoints();
app.MapGroup("api/v1/tracks").WithTags("Tracks").MapTracksEndpoints();
app.MapGroup("api/v1/search").WithTags("Search").MapSearchEndpoints();

app.Run();
