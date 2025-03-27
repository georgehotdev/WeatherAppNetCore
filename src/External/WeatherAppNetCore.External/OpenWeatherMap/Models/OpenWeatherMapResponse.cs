﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WeatherAppNetCore.External.OpenWeatherMap.Models;

internal record OpenWeatherMapResponse
{
    [JsonProperty("id")] public long Id { get; set; }

    [JsonProperty("name")] public string Location { get; set; }

    [JsonProperty("dt")]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime ForecastDate { get; set; }


    [JsonProperty("main")] public OpenWeatherMapForecastResponse WeatherForecast { get; set; }
}