namespace FirstModule.SecondTask.Entities;

public class ProductEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
}