using Ardalis.Result;
using MediatR;
using WeatherAppNetCore.CommandStack.Mappers;
using WeatherAppNetCore.Domain;
using WeatherAppNetCore.Persistence;

namespace WeatherAppNetCore.CommandStack.WeatherForecasts;

public class StoreWeatherForecastsCommand : IRequest<Result>
{
    public StoreWeatherForecastsCommand(IEnumerable<WeatherForecast> weatherForecasts)
    {
        WeatherForecasts = weatherForecasts;
    }

    public IEnumerable<WeatherForecast> WeatherForecasts { get; }
}

public class StoreWeatherForecastCommandHandler : IRequestHandler<StoreWeatherForecastsCommand, Result>
{
    private readonly WeatherAppDbContext _dbContext;

    public StoreWeatherForecastCommandHandler(WeatherAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result> Handle(StoreWeatherForecastsCommand request, CancellationToken cancellationToken)
    {
        var weatherForecastEntities = request.WeatherForecasts.Select(w => w.AsEntity());

        try
        {
            _dbContext.WeatherForecasts.AddRange(weatherForecastEntities);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            return Result.CriticalError(ex.Message);
        }

        return Result.Success();
    }
}