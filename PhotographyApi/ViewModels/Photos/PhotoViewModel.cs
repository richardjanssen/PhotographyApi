namespace PhotographyApi.ViewModels.Photos;

public class PhotoViewModel
{
    public PhotoViewModel(int? id, DateTime? date, IReadOnlyCollection<ImageViewModel> images)
    {
        Id = id;
        Date = date;
        Images = images;
    }

    public int? Id { get; }
    public DateTime? Date { get; }
    public IReadOnlyCollection<ImageViewModel> Images { get; }
}
