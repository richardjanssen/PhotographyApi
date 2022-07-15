namespace PhotographyApi.ViewModels;

public class AddPhotoViewModel
{
    public AddPhotoViewModel(IReadOnlyCollection<ImageViewModel> images)
    {
        Images = images;
    }
    public IReadOnlyCollection<ImageViewModel> Images { get; set; }
}
