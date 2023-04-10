namespace Business.Entities;

public class HikerLocation
{
    public HikerLocation(int id, DateTime date, double distance)
    {
        Id = id;
        Date = date;
        Distance = distance;
    }

    public int Id { get; }
    public DateTime Date { get; }
    public double Distance { get; }
}
