namespace WeatherAppNetCore.Domain;

public class WeatherForecast
{
    public int Id { get; set; }
    public long ForecastId { get; set; }
    public string Location { get; set; }
    public DateTime ForecastDate { get; set; }
    public double CurrentTemperature { get; set; }
    public double MinTemperature { get; set; }
    public double MaxTemperature { get; set; }
}