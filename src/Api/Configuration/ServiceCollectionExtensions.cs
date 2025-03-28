using Microsoft.EntityFrameworkCore;
using WeatherAppNetCore.Api.BackgroundServices;
using WeatherAppNetCore.Application;
using WeatherAppNetCore.Application.Abstractions;
using WeatherAppNetCore.CommandStack.WeatherForecasts;
using WeatherAppNetCore.External.Abstractions;
using WeatherAppNetCore.External.OpenWeatherMap;
using WeatherAppNetCore.Infrastructure.Abstractions;
using WeatherAppNetCore.Infrastructure.Configuration;
using WeatherAppNetCore.Infrastructure.Http;
using WeatherAppNetCore.Persistence;
using WeatherAppNetCore.QueryStack;
using WeatherAppNetCore.QueryStack.Abstractions;

namespace WeatherAppNetCore.Api.Configuration;

public static class ServiceCollectionExtensions
{
    public static WebApplicationBuilder AddInfrastructureDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHttpClient();
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<StoreWeatherForecastsCommand>();
            cfg.RegisterServicesFromAssemblyContaining<IWeatherForecastQueryStack>();
        });
        builder.Services.AddDbContext<WeatherAppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

        builder.Services.AddScoped<IHttpService, HttpService>();

        return builder;
    }

    public static WebApplicationBuilder AddBackgroundServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddHostedService<WeatherFetcherBackgroundService>();

        return builder;
    }
    
    public static WebApplicationBuilder AddExternalServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IWeatherFetcher, OpenWeatherMapWeatherFetcher>();

        return builder;
    }
    
    public static WebApplicationBuilder AddConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<WeatherApiConfig>(builder.Configuration.GetSection(nameof(WeatherApiConfig)));

        return builder;
    }
    
    public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();
        builder.Services.AddScoped<IWeatherForecastQueryStack, WeatherForecastQueryStack>();

        return builder;
    }
}