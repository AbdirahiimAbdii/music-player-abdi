using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Music.Api.Features.Images;

public static class Images
{
    public static IEndpointRouteBuilder MapImagesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:int}", GetImageById).WithName(nameof(GetImageById));
        app.MapPost("/", AddImage).WithName(nameof(AddImage));
        app.MapDelete("/{id:int}", RemoveImage).WithName(nameof(RemoveImage));
        return app;
    }
    
    private static string GetImageById(int id)
    {
        return $"fetch image with id = {id}";
    }

    private static string AddImage()
    {
        return "add new image";
    }
    
    private static string RemoveImage(int id)
    {
        return $"deleting image with id = {id}";
    }
}