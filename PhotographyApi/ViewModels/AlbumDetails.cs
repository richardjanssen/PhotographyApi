namespace PhotographyApi.ViewModels;

public class AlbumDetailsViewModel
{
    public AlbumDetailsViewModel(IReadOnlyCollection<PhotoViewModel> photos)
    {
        Photos = photos;
    }

    public IReadOnlyCollection<PhotoViewModel> Photos { get; }
}
