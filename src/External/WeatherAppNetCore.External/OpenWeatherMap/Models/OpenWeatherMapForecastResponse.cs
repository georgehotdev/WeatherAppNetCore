﻿using Newtonsoft.Json;

namespace WeatherAppNetCore.External.OpenWeatherMap.Models;

internal record OpenWeatherMapForecastResponse
{
    [JsonProperty("temp")] public double CurrentTemperature { get; set; }

    [JsonProperty("temp_min")] public double MinTemperature { get; set; }

    [JsonProperty("temp_max")] public double MaxTemperature { get; set; }
}