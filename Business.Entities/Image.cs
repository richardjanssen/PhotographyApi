namespace Business.Entities;

public class Image
{
    public Image(int widthPx, int heightPx, Guid guid, string extension)
    {
        WidthPx = widthPx;
        HeightPx = heightPx;
        Guid = guid;
        Extension = extension;
    }

    public int WidthPx { get; set; }
    public int HeightPx { get; set; }
    public Guid Guid { get; set; }
    public string Extension { get; set; }
}
