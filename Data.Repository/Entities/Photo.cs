using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Repository.Entities;

[Table("Photos")]
public class Photo
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public ICollection<Image> Images { get; set; } = new List<Image>();
}
