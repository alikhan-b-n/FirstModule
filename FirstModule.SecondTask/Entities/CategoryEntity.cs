namespace FirstModule.SecondTask.Entities;

public class CategoryEntity
{
    public string Title { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
}