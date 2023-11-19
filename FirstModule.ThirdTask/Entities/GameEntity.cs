using Microsoft.EntityFrameworkCore;

namespace FirstModule.ThirdTask.Entities;

public class GameEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid HomeId { get; set; }
    public Guid VisitorsId { get; set; }
    public DateTime DateOfEvent { get; set; }
    public Guid ScoreId { get; set; }

}