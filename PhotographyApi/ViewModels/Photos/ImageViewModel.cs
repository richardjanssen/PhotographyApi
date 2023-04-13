namespace PhotographyApi.ViewModels.Photos;

public class ImageViewModel
{
    public ImageViewModel(int widthPx, int heightPx, string path)
    {
        WidthPx = widthPx;
        HeightPx = heightPx;
        Path = path;
    }

    public int WidthPx { get; }
    public int HeightPx { get; }
    public string Path { get; }
}
