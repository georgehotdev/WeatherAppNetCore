import React, { useEffect, useState } from 'react'
import Card from './card/Card'
import CardBody from './card/CardBody'
import CardHeader from './card/CardHeader'
import CardSection from './card/CardSection'
import CardSectionHeader from './card/CardSectionHeader'
import CardSubtitle from './card/CardSubtitle'
import CardTitle from './card/CardTitle'
import Carousel from './carousel/Carousel'
import CarouselItem from './carousel/CarouselItem'
import ComboBox from './common/ComboBox'
import { getLocations, getWeather } from '@services/WeatherService'
import { WeatherForecast } from '@/models/WeatherForecast'
import moment from 'moment'
import { formatTemperature } from '@utils/temperatureFormatter'

export default function AppContainer() {
  const [locations, setLocations] = useState<string[]>([])
  const [nextWeatherForecasts, setNextWeatherForecasts] =
    useState<WeatherForecast[]>()
  const [currentWeatherReport, setCurrentWeatherReport] =
    useState<WeatherForecast>()
  const [selectedLocation, setSelectedLocation] = useState<string>('')

  const loadLocations = async () => {
    setLocations(await getLocations())
  }

  const getWeatherForecast = async (location: string) => {
    const allWeatherForecasts = await getWeather(location)
    setCurrentWeatherReport(allWeatherForecasts[0])
    setNextWeatherForecasts(allWeatherForecasts.slice(1, 4))
  }

  useEffect(() => {
    loadLocations()
  }, [])

  useEffect(() => {
    if (selectedLocation) {
      getWeatherForecast(selectedLocation)
    }
  }, [selectedLocation])

  return (
    <div
      id="root-container"
      className="flex min-h-screen items-center justify-center bg-gradient-to-br from-blue-900 to-blue-700 p-4"
    >
      <Card>
        <CardHeader>
          <CardTitle text={selectedLocation}></CardTitle>
          <CardSubtitle
            text={formatTemperature(currentWeatherReport?.currentTemperature)}
          ></CardSubtitle>
        </CardHeader>
        <CardBody>
          <CardSection>
            <CardSectionHeader text="3-Day Forecast"></CardSectionHeader>
            <Carousel>
              {nextWeatherForecasts?.map((weatherForecast, index) => (
                <CarouselItem
                  key={index}
                  heading={moment(weatherForecast.forecastDate).format('ddd')}
                  paragraphs={[
                    formatTemperature(weatherForecast.currentTemperature),
                    `${formatTemperature(
                      weatherForecast.minTemperature,
                      false
                    )} - ${formatTemperature(
                      weatherForecast.maxTemperature,
                      false
                    )}`
                  ]}
                ></CarouselItem>
              ))}
            </Carousel>
          </CardSection>
          <div className="mb-4 h-12">
            <ComboBox
              values={locations}
              onChange={setSelectedLocation}
            ></ComboBox>
          </div>
        </CardBody>
      </Card>
    </div>
  )
}
