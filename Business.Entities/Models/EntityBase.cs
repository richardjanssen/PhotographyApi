namespace Business.Entities.Models;
public class EntityBase
{
    public long Id { get; private set; }
    public long RowVersion { get; set; }
    public DateTime DateModifiedUtc { get; set; }
}
