using Ardalis.Result;
using WeatherAppNetCore.Domain;

namespace WeatherAppNetCore.Application.Abstractions;

public interface IWeatherForecastService
{
    Task<IEnumerable<WeatherForecast>> GetWeatherForecastAsync(string city);
    Task<IEnumerable<string>> GetLocationsAsync();
}