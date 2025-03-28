using Microsoft.EntityFrameworkCore;
using WeatherAppNetCore.Persistence.Entities;

namespace WeatherAppNetCore.Persistence;

public class WeatherAppDbContext : DbContext
{
    public WeatherAppDbContext(DbContextOptions<WeatherAppDbContext> options) : base(options)
    {
    }

    public DbSet<WeatherForecastEntity> WeatherForecasts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<WeatherForecastEntity>()
            .HasIndex(x => x.ForecastReferenceId)
            .IsUnique();
    }
}