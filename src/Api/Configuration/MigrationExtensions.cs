using Microsoft.EntityFrameworkCore;
using WeatherAppNetCore.Persistence;

namespace WeatherAppNetCore.Api.Configuration;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        using var dbContext =
            scope.ServiceProvider.GetRequiredService<WeatherAppDbContext>();

        dbContext.Database.Migrate();
    }
}