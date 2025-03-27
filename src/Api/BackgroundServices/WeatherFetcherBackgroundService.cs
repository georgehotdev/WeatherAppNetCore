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
    private readonly IMediator _bus;
    private readonly ILogger<WeatherFetcherBackgroundService> _logger;
    private readonly WeatherApiConfig _weatherApiConfig;
    private readonly IWeatherFetcher _weatherFetcher;

    public WeatherFetcherBackgroundService(ILogger<WeatherFetcherBackgroundService> logger,
        IWeatherFetcher weatherFetcher, IOptions<WeatherApiConfig> weatherApiConfig,
        IMediator bus)
    {
        _logger = logger;
        _weatherFetcher = weatherFetcher;
        _bus = bus;
        _weatherApiConfig = weatherApiConfig.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            _logger.LogInformation("WeatherFetcherBackgroundService running at: {time}", DateTimeOffset.Now);

            var weatherForecastFetchResult = await FetchWeatherData();

            if (!weatherForecastFetchResult.IsSuccess)
            {
                LogFetchErrors(weatherForecastFetchResult);
            }
            else
            {
                await StoreWeatherForecasts(weatherForecastFetchResult);
            }
            await Task.Delay(TimeSpan.FromSeconds(_weatherApiConfig.IntervalBetweenRequestsInSeconds),
                cancellationToken);
        }
    }

    private async Task StoreWeatherForecasts(Result<List<WeatherForecast>> weatherForecastFetchResult)
    {
        await _bus.Send(new StoreWeatherForecastsCommand(weatherForecastFetchResult.Value));

        _logger.LogInformation("WeatherFetcherBackgroundService completed at: {time}", DateTimeOffset.Now);
    }

    private void LogFetchErrors(Result<List<WeatherForecast>> weatherForecastFetchResult)
    {
        var errors = weatherForecastFetchResult.Errors.Aggregate((l, r) => $"{l}\n{r}");
        _logger.LogError("WeatherFetcherBackgroundService failed to fetch weather data: {error}", errors);
    }

    private async Task<Result<List<WeatherForecast>>> FetchWeatherData()
    {
        var locations = _weatherApiConfig.Locations;
        var batches = locations.Chunk(_weatherApiConfig.MaxDegreeOfParallelism);

        try
        {
            var weatherForecasts = new List<WeatherForecast>();
            foreach (var batch in batches)
            {
                var tasks = batch.Select(location => _weatherFetcher.GetWeatherForecastAsync(location.City)).ToList();
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