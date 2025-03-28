using MediatR;
using WeatherAppNetCore.Application.Abstractions;
using WeatherAppNetCore.Domain;
using WeatherAppNetCore.QueryStack.Queries;

namespace WeatherAppNetCore.Application;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly IMediator _bus;

    public WeatherForecastService(IMediator bus)
    {
        _bus = bus;
    }
    
    public async Task<IEnumerable<WeatherForecast>> GetWeatherForecastAsync(string city)
    {
        return await _bus.Send(new GetWeatherForecastsQuery(city));
    }

    public async Task<IEnumerable<string>> GetLocationsAsync()
    {
        return await _bus.Send(new GetLocationsQuery());
    }
}