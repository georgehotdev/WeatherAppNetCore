using MediatR;
using Microsoft.EntityFrameworkCore;
using WeatherAppNetCore.Domain;
using WeatherAppNetCore.QueryStack.Abstractions;
using WeatherAppNetCore.QueryStack.Mappers;

namespace WeatherAppNetCore.QueryStack.Queries;

public class GetWeatherForecastsQuery : IRequest<IEnumerable<WeatherForecast>>
{
    public string Location { get; }

    public GetWeatherForecastsQuery(string location)
    {
        Location = location;
    }
}

public class GetWeatherForecastsQueryHandler : IRequestHandler<GetWeatherForecastsQuery, IEnumerable<WeatherForecast>>
{
    private readonly IWeatherForecastQueryStack _weatherForecastQueryStack;

    public GetWeatherForecastsQueryHandler(IWeatherForecastQueryStack weatherForecastQueryStack)
    {
        _weatherForecastQueryStack = weatherForecastQueryStack;
    }

    public async Task<IEnumerable<WeatherForecast>> Handle(GetWeatherForecastsQuery request,
        CancellationToken cancellationToken)
    {
        return await _weatherForecastQueryStack.WeatherForecasts
            .Where(wf => wf.Location == request.Location)
            .OrderBy(wf => wf.ForecastDate)
            .Select(wf => new WeatherForecast
            {
                Id = wf.Id,
                ForecastDate = wf.ForecastDate,
                Location = wf.Location,
                CurrentTemperature = wf.CurrentTemperature,
                MinTemperature = wf.MinTemperature,
                MaxTemperature = wf.MaxTemperature,
            })
            .ToListAsync(cancellationToken);
    }
}