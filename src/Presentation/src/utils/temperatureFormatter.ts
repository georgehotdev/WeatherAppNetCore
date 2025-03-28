export const formatTemperature = (
  temperature?: number,
  includeEmoji: boolean = true
) => {
  if (!temperature) {
    return ''
  }

  if (!includeEmoji) {
    return `${temperature.toFixed(0)}°C`
  }
  if (temperature < 10) {
    return `❄️ ${temperature.toFixed(0)}°C`
  }
  if (temperature >= 10 && temperature <= 15) {
    return `🌡️ ${temperature.toFixed(0)}°C`
  }
  if (temperature > 15) {
    return `☀️ ${temperature.toFixed(0)}°C`
  }

  return `${temperature.toFixed(0)}°C`
}
