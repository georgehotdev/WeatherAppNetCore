using WeatherAppNetCore.Domain;
using WeatherAppNetCore.Persistence.Entities;

namespace WeatherAppNetCore.CommandStack.Mappers;

public static class WeatherForecastMapping
{
    public static WeatherForecastEntity AsEntity(this WeatherForecast weatherForecast)
    {
        return new WeatherForecastEntity
        {
            ForecastReferenceId = weatherForecast.ForecastId,
            CurrentTemperature = weatherForecast.CurrentTemperature,
            ForecastDate = weatherForecast.ForecastDate,
            Location = weatherForecast.Location,
            MaxTemperature = weatherForecast.MaxTemperature,
            MinTemperature = weatherForecast.MinTemperature
        };
    }
}