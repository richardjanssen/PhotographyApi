namespace PhotographyApi.ViewModels;

public class AddPhotoViewModel
{
    public AddPhotoViewModel(IReadOnlyCollection<ImageWithPathViewModel> images)
    {
        Images = images;
    }
    public IReadOnlyCollection<ImageWithPathViewModel> Images { get; set; }
}
