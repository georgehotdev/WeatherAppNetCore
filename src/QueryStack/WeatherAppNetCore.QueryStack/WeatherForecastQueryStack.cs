using Microsoft.EntityFrameworkCore;
using WeatherAppNetCore.Persistence;
using WeatherAppNetCore.Persistence.Entities;
using WeatherAppNetCore.QueryStack.Abstractions;

namespace WeatherAppNetCore.QueryStack;

public class WeatherForecastQueryStack : IWeatherForecastQueryStack
{
    private readonly WeatherAppDbContext _dbContext;

    public WeatherForecastQueryStack(WeatherAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<WeatherForecastEntity> WeatherForecasts => _dbContext.WeatherForecasts.AsNoTracking();
}