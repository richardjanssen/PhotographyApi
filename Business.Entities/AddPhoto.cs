using Microsoft.AspNetCore.Http;

namespace Business.Entities;

public class AddPhoto
{
    public AddPhoto(int? albumId, IFormFile image)
    {
        AlbumId = albumId;
        Image = image;
    }
    public int? AlbumId { get; }
    public IFormFile Image { get; }
}
