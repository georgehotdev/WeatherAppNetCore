using Ardalis.Result;

namespace WeatherAppNetCore.Infrastructure.Abstractions;

public interface IHttpService
{
    Task<Result<T?>> GetAsync<T>(string url);
}