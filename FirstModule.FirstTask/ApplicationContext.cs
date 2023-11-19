using FirstModule.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirstModule;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<EmployeeEntity> Employies { get; set; }
    public DbSet<SkillEntity> Skills { get; set; }
}