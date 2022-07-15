namespace Business.Entities;

public class AddPhoto
{
    public AddPhoto(IReadOnlyCollection<Image> images)
    {
        Images = images;
    }
    public IReadOnlyCollection<Image> Images { get; }
}
