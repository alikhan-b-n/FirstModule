namespace FirstModule.ThirdTask.Entities;

public class TeamsEntity
{
    public string Name { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
}