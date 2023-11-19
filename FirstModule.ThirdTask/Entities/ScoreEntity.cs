namespace FirstModule.ThirdTask.Entities;

public class ScoreEntity
{
    public int? HomeScore { get; set; }
    public int? VisitorsScore { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
}