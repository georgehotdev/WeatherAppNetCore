namespace WeatherAppNetCore.Persistence.Entities;

public class WeatherForecastEntity
{
    public int Id { get; set; }
    public long? ForecastReferenceId { get; set; }
    public string Location { get; set; }
    public DateTime ForecastDate { get; set; }
    public double CurrentTemperature { get; set; }
    public double MinTemperature { get; set; }
    public double MaxTemperature { get; set; }
}