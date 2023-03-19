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

    public int WidthPx { get; }
    public int HeightPx { get; }
    public Guid Guid { get; }
    public string Extension { get; }
}
