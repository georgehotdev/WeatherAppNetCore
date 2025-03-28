using WeatherAppNetCore.Domain;
using WeatherAppNetCore.Persistence.Entities;

namespace WeatherAppNetCore.QueryStack.Mappers;

public static class WeatherForecastMapping
{
    public static WeatherForecast AsDomain(this WeatherForecastEntity weatherForecast)
    {
        return new WeatherForecast
        {
            Id = weatherForecast.Id,
            CurrentTemperature = weatherForecast.CurrentTemperature,
            ForecastDate = weatherForecast.ForecastDate,
            Location = weatherForecast.Location,
            MaxTemperature = weatherForecast.MaxTemperature,
            MinTemperature = weatherForecast.MinTemperature
        };
    }
}