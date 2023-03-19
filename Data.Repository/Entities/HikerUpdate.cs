using Common.Common.Enums;

namespace Data.Repository.Entities;

public class HikerUpdate
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public PlaceHighlightType Type { get; set; }
    public string? Text { get; set; }
    public double Distance { get; set; }
    public int? AlbumId { get; set; }
    
}
