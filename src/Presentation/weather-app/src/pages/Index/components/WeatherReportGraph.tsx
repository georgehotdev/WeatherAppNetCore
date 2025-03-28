import React from 'react'

import { WeatherForecast } from '@/models/WeatherForecast'
import LineChart, {
  LineChartProps,
  LineChartSerie
} from '@components/lineChart/LineChart'
import moment from 'moment'

type WeatherReportGraphProps = {
  weatherForecasts: WeatherForecast[]
}

export default function WeatherReportGraph({
  weatherForecasts
}: WeatherReportGraphProps) {
  const series: LineChartSerie[] = [
    {
      label: 'Max Temp (°C)',
      values: weatherForecasts.map((forecast) => forecast.maxTemperature)
    },
    {
      label: 'Min Temp (°C)',
      values: weatherForecasts.map((forecast) => forecast.minTemperature)
    }
  ]

  const lineChartData: LineChartProps = {
    labels: weatherForecasts.map((forecast) =>
      moment(forecast.forecastDate).format('MMM D')
    ),
    series
  }

  return <LineChart {...lineChartData} />
}
