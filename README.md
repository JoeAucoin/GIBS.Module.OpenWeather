# GIBS OpenWeather Module for Oqtane

A Blazor module for [Oqtane CMS](https://www.oqtane.org) that displays real-time weather data, forecasts, and interactive charts using the [OpenWeather API](https://openweathermap.org/api).

## Features

- **Current Conditions**: Temperature, humidity, wind, pressure, and more
- **Hourly & Daily Forecasts**: Interactive charts for temperature and wind
- **Weather Alerts**: Displays active weather alerts
- **Map Integration**: Location visualization with Leaflet maps
- **Location Lookup**: Search latitude/longitude by city, state, and country
- **Imperial/Metric Units**: User-selectable units
- **Localization**: Multi-language support via Oqtane

## Getting Started

### Prerequisites

- Oqtane CMS v6.1.2+  
- .NET 9 SDK  
- [OpenWeather API Key](https://home.openweathermap.org/users/sign_up)

### Installation

1. **Clone the repository**
    ```sh
    git clone https://github.com/JoeAucoin/GIBS.Module.OpenWeather.git
    ```
2. **Build the solution**
    - Open in Visual Studio
    - Build the project

3. **Add the module to your Oqtane site**
    - Log in as an admin
    - Add the "GIBS OpenWeather" module to a page

### Configuration

1. Go to module settings
2. Enter your OpenWeather API key
3. Specify city, state, and country
4. Use "Lookup Lat/Long" to auto-fill coordinates
5. Select units (Imperial/Metric)
6. Adjust map zoom level as needed

## Usage

- View current weather, forecasts, and alerts for your location
- Use interactive charts for hourly and daily trends
- Change settings to update displayed data

## File Structure

- `Client/Modules/GIBS.Module.OpenWeather/Index.razor` – Main UI
- `Client/Modules/GIBS.Module.OpenWeather/Settings.razor` – Settings UI
- `Client/Services/WeatherProvider.cs` – API logic
- `Shared/Models/OpenWeather.cs` – Data models

## Development

- Built with Blazor and Oqtane module conventions
- Uses dependency injection for services
- Async/await for API calls and error handling

## Contributing

Pull requests and issues are welcome!  
Please follow Oqtane module development guidelines.

## License

MIT License © .NET Foundation

---
