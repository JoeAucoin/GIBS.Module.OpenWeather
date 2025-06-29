namespace GIBS.Module.OpenWeather.Models
{
    public class HourlyWeatherInfo
    {
        public long Dt { get; set; } // Unix timestamp for the hourly data  
        public double Temp { get; set; } // Temperature in Fahrenheit  
        public double WindSpeed { get; set; } // Wind speed in mph  
        public double WindGust { get; set; } // Wind gust in mph  
    }
}
