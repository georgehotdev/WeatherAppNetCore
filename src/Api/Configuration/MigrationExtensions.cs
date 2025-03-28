using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Polly;
using WeatherAppNetCore.Infrastructure.Configuration;
using WeatherAppNetCore.Persistence;
using WeatherAppNetCore.Persistence.Entities;

namespace WeatherAppNetCore.Api.Configuration;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        var policy = Policy
            .Handle<Exception>()
            .WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(5));

        policy.Execute(() =>
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<WeatherAppDbContext>();

            dbContext.Database.Migrate();
        });
    }

    public static void SeedData(this IApplicationBuilder app)
    {
        int seedUniqueIdentifier = 1;
        using var scope = app.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<WeatherAppDbContext>();
        var configuration = scope.ServiceProvider.GetRequiredService<IOptions<WeatherApiConfig>>();

        if (!dbContext.WeatherForecasts.Any())
        {
            var seedWeatherForecasts = new List<WeatherForecastEntity>();

            foreach (var location in configuration.Value.Locations)
            {
                var past7DaysData = Enumerable.Range(0, 6)
                    .Select(x => DateTime.Now.AddDays(x))
                    .Select(date =>
                    {
                        var random = new Random();
                        var minTemperature = random.Next(0, 15);
                        var maxTemperature = random.Next(15, 30);

                        return new WeatherForecastEntity
                        {
                            Location = location.City,
                            CurrentTemperature = random.Next(minTemperature, maxTemperature),
                            ForecastDate = date.ToUniversalTime(),
                            MaxTemperature = maxTemperature,
                            MinTemperature = minTemperature,
                            ForecastReferenceId = seedUniqueIdentifier++
                        };
                    });

                seedWeatherForecasts.AddRange(past7DaysData);
            }

            dbContext.WeatherForecasts.AddRange(seedWeatherForecasts);
            dbContext.SaveChanges();
        }
    }
}