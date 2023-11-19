namespace FirstModule.Dtos;

public class EmployeeResponseDto
{
    public string FullName { get; set; }
    public string? Skills { get; set; }
    public Guid Id { get; set; }
}