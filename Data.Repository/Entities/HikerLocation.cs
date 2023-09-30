namespace Data.Repository.Entities;

public class HikerLocation
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public bool IsManual { get; set; }
    public double Lat { get; set; }
    public double Lon { get; set; }
    public int? PlaceId { get; set; }
}
