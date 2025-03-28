import React, { useEffect, useState } from 'react'
import Card from '../../components/card/Card'
import CardBody from '../../components/card/CardBody'
import CardHeader from '../../components/card/CardHeader'
import CardSection from '../../components/card/CardSection'
import CardSectionHeader from '../../components/card/CardSectionHeader'
import CardSubtitle from '../../components/card/CardSubtitle'
import CardTitle from '../../components/card/CardTitle'
import Carousel from '../../components/carousel/Carousel'
import CarouselItem from '../../components/carousel/CarouselItem'
import ComboBox from '../../components/combobox/ComboBox'
import { getLocations, getWeather } from '@services/WeatherService'
import { WeatherForecast } from '@/models/WeatherForecast'
import moment from 'moment'
import { formatTemperature } from '@utils/temperatureFormatter'
import WeatherReportGraph from '@pages/Index/components/WeatherReportGraph'
import Spinner from '@components/spinner/Spinner'

export default function IndexPageContainer() {
  const [locations, setLocations] = useState<string[]>([])
  const [allWeatherForecasts, setAllWeatherForecasts] =
    useState<WeatherForecast[]>()
  const [upcomingWeatherForecasts, setUpcomingWeatherForecasts] =
    useState<WeatherForecast[]>()
  const [todayWeatherForecast, setTodayWeatherForecast] =
    useState<WeatherForecast>()
  const [selectedLocation, setSelectedLocation] = useState<string>('')

  const loadLocations = async () => {
    setLocations(await getLocations())
  }

  const getWeatherForecast = async (location: string) => {
    const allWeatherForecasts = await getWeather(location)
    setAllWeatherForecasts(allWeatherForecasts)
    setTodayWeatherForecast(allWeatherForecasts[0])
    setUpcomingWeatherForecasts(allWeatherForecasts.slice(1, 4))
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
            text={formatTemperature(todayWeatherForecast?.currentTemperature)}
          ></CardSubtitle>
        </CardHeader>
        {locations && (
          <CardBody>
            <CardSection>
              <CardSectionHeader text="3-Day Forecast"></CardSectionHeader>
              <Carousel>
                {upcomingWeatherForecasts?.map((wf, index) => (
                  <CarouselItem
                    key={index}
                    heading={moment(wf.forecastDate).format('ddd')}
                    paragraphs={[
                      formatTemperature(wf.currentTemperature),
                      `${formatTemperature(
                        wf.minTemperature,
                        false
                      )} - ${formatTemperature(wf.maxTemperature, false)}`
                    ]}
                  ></CarouselItem>
                ))}
              </Carousel>
            </CardSection>
            <CardSection>
              {allWeatherForecasts && (
                <WeatherReportGraph
                  weatherForecasts={allWeatherForecasts}
                ></WeatherReportGraph>
              )}
            </CardSection>
            <div className="mb-4 h-12">
              <ComboBox
                values={locations}
                onChange={setSelectedLocation}
                initSelectedValue={true}
              ></ComboBox>
            </div>
          </CardBody>
        )}
        {!locations && <Spinner></Spinner>}
      </Card>
    </div>
  )
}
