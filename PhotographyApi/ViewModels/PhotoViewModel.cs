namespace PhotographyApi.ViewModels;

public class PhotoViewModel
{
    public PhotoViewModel(int? id, DateTime? date, IReadOnlyCollection<ImageWithPathViewModel> images)
    {
        Id = id;
        Date = date;
        Images = images;
    }

    public int? Id { get; set; }
    public DateTime? Date { get; set; }
    public IReadOnlyCollection<ImageWithPathViewModel> Images { get; set; }
}
