namespace Business.Entities.Dto;

public class Section
{
    public Section(int id, string title, double startDistance, double endDistance)
    {
        Id = id;
        Title = title;
        StartDistance = startDistance;
        EndDistance = endDistance;
    }

    public int Id { get; }
    public string Title { get; }
    public double StartDistance { get; }
    public double EndDistance { get; }

}
