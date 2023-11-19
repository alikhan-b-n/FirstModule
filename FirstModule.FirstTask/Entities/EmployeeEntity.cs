namespace FirstModule.Entities;

public class EmployeeEntity
{
    public Guid Id { get; set; } =Guid.NewGuid();
    public string FullName { get; set; }
}