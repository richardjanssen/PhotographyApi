namespace Data.Repository.Entities;

public class Image
{
    public int WidthPx { get; set; }
    public int HeightPx { get; set; }
    public Guid Guid { get; set; }
    public string Extension { get; set; } = null!;
}
