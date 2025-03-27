using WeatherAppNetCore.Persistence.Entities;

namespace WeatherAppNetCore.QueryStack.Abstractions;

public interface IWeatherForecastQueryStack
{
    public IQueryable<WeatherForecastEntity> WeatherForecasts { get; }
}