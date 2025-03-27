using Ardalis.Result;
using WeatherAppNetCore.Domain;

namespace WeatherAppNetCore.External.Abstractions;

public interface IWeatherFetcher
{
    Task<Result<WeatherForecast?>> GetWeatherForecastAsync(string city);
}