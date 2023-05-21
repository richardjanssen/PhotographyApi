namespace Business.Entities.Dto;

public class HikerLocation
{
    public HikerLocation(int id,
        DateTime date,
        double? actualDistance,
        double? roundedDistance,
        bool isManual,
        double? lat,
        double? lon)
    {
        Id = id;
        Date = date;
        ActualDistance = actualDistance;
        RoundedDistance = roundedDistance;
        IsManual = isManual;
        Lat = lat;
        Lon = lon;
    }

    public HikerLocation(DateTime date,
        double? actualDistance,
        double? roundedDistance,
        bool isManual,
        double? lat,
        double? lon)
    {
        Date = date;
        ActualDistance = actualDistance;
        RoundedDistance = roundedDistance;
        IsManual = isManual;
        Lat = lat;
        Lon = lon;
    }

    public int Id { get; }
    public DateTime Date { get; }
    public double? ActualDistance { get; }
    public double? RoundedDistance { get; }
    public bool IsManual { get; }
    public double? Lat { get; }
    public double? Lon { get; }
}
