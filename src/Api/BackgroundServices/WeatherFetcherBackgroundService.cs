using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Options;
using WeatherAppNetCore.CommandStack.WeatherForecasts;
using WeatherAppNetCore.Domain;
using WeatherAppNetCore.External.Abstractions;
using WeatherAppNetCore.Infrastructure.Configuration;

namespace WeatherAppNetCore.Api.BackgroundServices;

public class WeatherFetcherBackgroundService : BackgroundService
{
    private readonly ILogger<WeatherFetcherBackgroundService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly WeatherApiConfig _weatherApiConfig;

    public WeatherFetcherBackgroundService(ILogger<WeatherFetcherBackgroundService> logger, IOptions<WeatherApiConfig> weatherApiConfig, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _weatherApiConfig = weatherApiConfig.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var weatherFetcher = scope.ServiceProvider.GetRequiredService<IWeatherFetcher>();
                var bus = scope.ServiceProvider.GetRequiredService<IMediator>();
                
                _logger.LogInformation("WeatherFetcherBackgroundService running at: {time}", DateTimeOffset.Now);

                var weatherForecastFetchResult = await FetchWeatherData(weatherFetcher);

                if (!weatherForecastFetchResult.IsSuccess)
                {
                    LogFetchErrors(weatherForecastFetchResult);
                }
                else
                {
                    await StoreWeatherForecasts(bus, weatherForecastFetchResult);
                }
            }
        
            
            await Task.Delay(TimeSpan.FromSeconds(_weatherApiConfig.IntervalBetweenRequestsInSeconds),
                cancellationToken);
        }
    }

    private async Task StoreWeatherForecasts(IMediator bus, Result<List<WeatherForecast>> weatherForecastFetchResult)
    {
        await bus.Send(new StoreWeatherForecastsCommand(weatherForecastFetchResult.Value));

        _logger.LogInformation("WeatherFetcherBackgroundService completed at: {time}", DateTimeOffset.Now);
    }

    private void LogFetchErrors(Result<List<WeatherForecast>> weatherForecastFetchResult)
    {
        var errors = weatherForecastFetchResult.Errors.Aggregate((l, r) => $"{l}\n{r}");
        _logger.LogError("WeatherFetcherBackgroundService failed to fetch weather data: {error}", errors);
    }

    private async Task<Result<List<WeatherForecast>>> FetchWeatherData(IWeatherFetcher weatherFetcher)
    {
        var locations = _weatherApiConfig.Locations;
        var batches = locations.Chunk(_weatherApiConfig.MaxDegreeOfParallelism);

        try
        {
            var weatherForecasts = new List<WeatherForecast>();
            foreach (var batch in batches)
            {
                var tasks = batch.Select(location => weatherFetcher.GetWeatherForecastAsync(location.City)).ToList();
                var result = await Task.WhenAll(tasks);
                var fetchedForecasts = result.Where(x => x.IsSuccess);
                weatherForecasts.AddRange(fetchedForecasts.Select(x => x.Value!));
            }

            return Result.Success(weatherForecasts);
        }
        catch (Exception ex)
        {
            return Result.CriticalError("Unable to retrieve weather data.");
        }
    }
}