using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Music.Api.Common.Entities;
using Music.Api.Persistence;

namespace Music.Api.Features.Images;

public static class Images
{
    public static IEndpointRouteBuilder MapImagesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:guid}", GetImageById)
            .WithName(nameof(GetImageById))
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
        
        app.MapPost("/", AddImage).WithName(nameof(AddImage));
        app.MapPut("/{id:guid}", EditImage).WithName(nameof(EditImage));
        app.MapDelete("/{id:guid}", RemoveImage).WithName(nameof(RemoveImage));
        return app;
    }
    
    private static async Task<IResult> GetImageById(Guid id, AppDbContext db)
    {
        var img = await db.Images.FindAsync(id);
        return img is null 
            ? Results.NotFound() 
            : Results.File(img.Content, img.Mime);
    }

    private static async Task<IResult> AddImage(
        AddImageRequest request,
        AppDbContext db,
        LinkGenerator linkGenerator,
        HttpContext http,
        CancellationToken ct)
    {
        var imgType = request.Type.ToLower();

        if (!Enum.TryParse<ImageType>(imgType, true, out var _))
            return Results.Problem($"Invalid image type {imgType}", statusCode: StatusCodes.Status400BadRequest);

        var img = new Image
        {
            Id = request.Id ?? Guid.NewGuid(),
            Mime = $"image/{imgType}",
            Description = request.Description,
            Content = Convert.FromBase64String(request.Content)
        };

        try
        {
            db.Images.Add(img);
            await db.SaveChangesAsync(ct);
            var uri = linkGenerator.GetUriByName(http, nameof(GetImageById), new { id = img.Id });
            return Results.Created(uri, img.Id);
        }
        catch
        {
            return Results.Problem("", statusCode: StatusCodes.Status500InternalServerError);
        }
    }
    
    private static async Task<IResult> EditImage(
        Guid id,
        EditImageRequest request,
        AppDbContext db,
        LinkGenerator linkGenerator,
        HttpContext http,
        CancellationToken ct)
    {
        var imgType = request.Type.ToLower();

        if (!Enum.TryParse<ImageType>(imgType, true, out var _))
            return Results.Problem($"Invalid image type {imgType}", statusCode: StatusCodes.Status400BadRequest);
        
        try
        {
            var img = await db.Images.FindAsync([id], ct);
            if (img is null)
            {
                return Results.NotFound();
            }

            img.Mime = $"image/{imgType}";
            img.Description = request.Description;
            img.Content = Convert.FromBase64String(request.Content);
            
            await db.SaveChangesAsync(ct);
            return Results.NoContent();
        }
        catch
        {
            return Results.Problem("", statusCode: StatusCodes.Status500InternalServerError);
        }
    }
    
    private static async Task<IResult> RemoveImage(Guid id, AppDbContext db, CancellationToken ct)
    {
        var img = await db.Images.FindAsync([id], ct);
        if (img is null)
            return Results.NotFound();

        db.Images.Remove(img);
        await db.SaveChangesAsync(ct);
        return Results.NoContent();
    }
}