namespace WeatherAppNetCore.Infrastructure.Configuration;

public record WeatherApiConfig
{
    private int _maxDegreeOfParallelism;
    public required string ApiKey { get; set; }
    public bool RunInParallel { get; set; }
    public required int IntervalBetweenRequestsInSeconds { get; set; }
    public required string BaseUrl { get; set; }
    public required IEnumerable<LocationConfig> Locations { get; set; }

    public int MaxDegreeOfParallelism
    {
        get => RunInParallel ? _maxDegreeOfParallelism : 1;
        set => _maxDegreeOfParallelism = value;
    }
}

public record LocationConfig
{
    public required string City { get; set; }
    public required string Country { get; set; }
}