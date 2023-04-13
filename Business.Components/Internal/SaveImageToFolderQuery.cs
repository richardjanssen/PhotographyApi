using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Image = Business.Entities.Dto.Image;
using SharpImage = SixLabors.ImageSharp.Image;

namespace Business.Components.Internal;

public class SaveImageToFolderQuery : ISaveImageToFolderQuery
{
    private readonly string _folderPath;
    private readonly Configuration _configuration;

    public SaveImageToFolderQuery(IWebHostEnvironment environment)
    {
        _folderPath = Path.Combine(environment.WebRootPath, "Images");
        _configuration = Configuration.Default;
        _configuration.MaxDegreeOfParallelism = 1;
    }

    public IReadOnlyCollection<Image> Execute(IFormFile file, IReadOnlyCollection<Entities.Size> maxDimensions)
    {
        if (file.Length == 0) { throw new Exception("Image size is 0"); }

        using SharpImage sharpImage = SharpImage.Load(_configuration, file.OpenReadStream());
        var extension = GetExtension(file.ContentType);

        var images = maxDimensions
            .Select(dimension =>
                {
                    var scaleFactor = GetScaleFactor(sharpImage, dimension);

                    if (scaleFactor > 1) { return null; } // No upscaling

                    var guid = Guid.NewGuid();    
                    var fullPath = Path.Combine(_folderPath, guid.ToString() + extension);

                    using SharpImage scaledImage = sharpImage.Clone(
                        ctx => ctx.Resize((int)(sharpImage.Width * scaleFactor), (int)(sharpImage.Height * scaleFactor)));

                    scaledImage.Save(fullPath);

                    return new Image(scaledImage.Width, scaledImage.Height, guid, extension);
                })
            .OfType<Image>()
            .ToList();

        if (images.Count == 0) {
            throw new Exception($"Image with width {sharpImage.Width}px and height {sharpImage.Height}px is too small.");
        }

        return images;
    }

    private static float GetScaleFactor(SharpImage image, Entities.Size maxDimensions)
    {
        var widthScaleFactor = image.Width / (float)maxDimensions.WidthPx;
        var heightScaleFactor = image.Height / (float)maxDimensions.HeightPx;
        return 1 / Math.Max(widthScaleFactor, heightScaleFactor);
    }
    private static string GetExtension(string contentType)
    {
        return contentType switch
        {
            "image/jpeg" => ".jpg",
            _ => throw new Exception($"ContentType {contentType} not supported"),
        };
    }
}
