using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Repository.Entities;

[Table("Images")]
public class Image
{
    public int Id { get; set; }
    public int WidthPx { get; set; }
    public int HeightPx { get; set; }
    public string Path { get; set; } = null!;
}
