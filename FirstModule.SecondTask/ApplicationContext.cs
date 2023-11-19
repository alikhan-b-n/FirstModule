using FirstModule.SecondTask.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirstModule.SecondTask;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions options) : base(options)
    {
        Database.Migrate();
    }

    public DbSet<CategoryEntity> CategoryEntities { get; set; }
    public DbSet<ProductEntity> ProductEntities { get; set; }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     
    // }
}