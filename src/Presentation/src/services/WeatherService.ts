import { WeatherForecast } from '@/models/WeatherForecast'
import axios from '@lib/axios'

export async function getLocations(): Promise<string[]> {
  try {
    const response = await axios.get('/locations')
    return response.data
  } catch (error) {
    throw new Error('Unable to fetch locations')
  }
}

export async function getWeather(location: string): Promise<WeatherForecast[]> {
  try {
    const response = await axios.get(`/${location}`, {
      params: {
        location
      }
    })

    return response.data
  } catch (error) {
    throw new Error('Unable to fetch weather data')
  }
}
