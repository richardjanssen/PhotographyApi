namespace PhotographyApi.ViewModels;

public class ImageViewModel
{
    public ImageViewModel(int widthPx, int heightPx, string path)
    {
        WidthPx = widthPx;
        HeightPx = heightPx;
        Path = path;
    }

    public int WidthPx { get; set; }
    public int HeightPx { get; set; }
    public string Path { get; set; }
}
