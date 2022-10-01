using Business.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Business.Components.Internal;

public class SaveImageToFolderQuery : ISaveImageToFolderQuery
{
    private readonly IWebHostEnvironment _environment;

    public SaveImageToFolderQuery(IWebHostEnvironment environment) => _environment = environment;

    public Image Execute(IFormFile file)
    {
        var folderName = "Images";
        var pathToSave = Path.Combine(_environment.WebRootPath, folderName);

        if (file.Length == 0) { throw new Exception("Image size is 0"); }

        var guid = Guid.NewGuid();
        var extension = GetExtension(file.ContentType);
        var fileName = guid.ToString() + extension;
        var fullPath = Path.Combine(pathToSave, fileName);
        using var stream = new FileStream(fullPath, FileMode.Create);
        file.CopyTo(stream);
        return new Image(100, 100, guid, extension);
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
