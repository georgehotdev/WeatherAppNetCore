using Microsoft.AspNetCore.Mvc;
using WeatherAppNetCore.Application.Abstractions;
using WeatherAppNetCore.Domain;

namespace WeatherAppNetCore.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherForecastService _weatherForecastService;
    
    public WeatherForecastController(IWeatherForecastService weatherForecastService)
    {
        _weatherForecastService = weatherForecastService;
    }

    /// <summary>
    /// Gets the weather forecasts for a specified city.
    /// </summary>
    /// <param name="location">The location for which to get the weather forecasts.</param>
    /// <returns>A list of weather forecasts for the specified city.</returns>
    /// <response code="200">Returns the list of weather forecasts</response>
    /// <response code="400">If the location is null or empty</response>
    /// <response code="500">If there is an internal server error</response>
    [HttpGet]
    [Route("{location}")]
    [ProducesResponseType(typeof(IEnumerable<WeatherForecast>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<WeatherForecast>>> GetForecasts(string location)
    {
        if (string.IsNullOrEmpty(location))
        {
            return BadRequest("Location cannot be null or empty.");
        }
    
        var weatherForecasts = await _weatherForecastService.GetWeatherForecastAsync(location);
        return Ok(weatherForecasts);
    }
    
    /// <summary>
    /// Gets the list of available locations.
    /// </summary>
    /// <returns>A list of available locations.</returns>
    /// <response code="200">Returns the list of locations</response>
    /// <response code="500">If there is an internal server error</response>
    [HttpGet("locations")]
    [ProducesResponseType(typeof(IEnumerable<string>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<string>>> GetLocations()
    {
        var locations = await _weatherForecastService.GetLocationsAsync();
        return Ok(locations);
    }
}