using Microsoft.AspNetCore.Http;

namespace Business.Entities;

public class AddPhoto(int albumId, IFormFile image)
{
    public int AlbumId { get; } = albumId;
    public IFormFile Image { get; } = image;
}
