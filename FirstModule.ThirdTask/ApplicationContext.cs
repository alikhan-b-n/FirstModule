using FirstModule.ThirdTask.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirstModule.ThirdTask;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions options) : base(options)
    {
        Database.Migrate();
    }

    public DbSet<TeamsEntity> Teams { get; set; }
    public DbSet<ScoreEntity> Scores { get; set; }
    public DbSet<GameEntity> Games { get; set; }
}