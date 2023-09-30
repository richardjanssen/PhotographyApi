using Common.Common.Enums;

namespace PhotographyApi.ViewModels.Highlights;

public class PointHighlightViewModel
{
    public PointHighlightViewModel(
        int id, 
        DateTime date,
        PointHighlightType type,
        string title,
        double? distance,
        bool isManual)
    {
        Id = id;
        Date = date;
        Type = type;
        Title = title;
        Distance = distance;
        IsManual = isManual;
    }

    public int Id { get; }
    public DateTime Date { get; }
    public PointHighlightType Type { get; }
    public string Title { get; }
    public double? Distance { get; }
    public bool IsManual { get; }
}