
# WeatherAppNetCore

## Requirements:

• Using any public weather API receive data (country, city, temperature) from 2 cities in 2-3 countries - with periodical update 1/min.
• Store this data in the database and show in graphs: Min and Max temperature (Country\City\Temperature\Last update time).

## How to Run the app
1. Open `docker-compose.override.yaml` file and replace `<YOUR_OPEN_WEATHER_API_KEY_GOES_HERE>`  with your own OpenWeatherMap API KEY for the WeatherApiConfig__ApiKey
2. Run `docker-compose -f docker-compose.yaml -f docker-compose.override.yaml up` command 
3. The frontend application runs on  `http://localhost:8080` 
4. Swagger documentation of the API can be found at `http://localhost:9000/swagger/index.html`

## Links
1. The application will run 