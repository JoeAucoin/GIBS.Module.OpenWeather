using GIBS.Module.OpenWeather.Models; // Ensure this using statement is added for the models
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace GIBS.Module.OpenWeather.Client.Services
{
    //internal class WeatherProvider
    //{
    //}

    internal class WeatherProvider
    {
        private readonly string _apiKey;
        private readonly double _latitude;
        private readonly double _longitude;
        private readonly string _units;
      // private readonly IHttpClientFactory _httpClientFactory;

        private const string ApiUrl = "https://api.openweathermap.org/data/3.0/onecall";
        private const string ApiOverviewUrl = "https://api.openweathermap.org/data/3.0/onecall/overview";
        private const string ApiGeoReverseUrl = "http://api.openweathermap.org/geo/1.0/reverse";

        private static readonly HttpClient httpClient = new HttpClient();

        public WeatherProvider(string apiKey, double latitude, double longitude, string units)
        {
            _apiKey = apiKey;
            _latitude = latitude;
            _longitude = longitude;
            _units = units;
        }

        public async Task<WeatherData> GetWeatherDataAsync()
        {
            string url = $"{ApiUrl}?lat={_latitude}&lon={_longitude}&exclude=minutely&appid={_apiKey}&units={_units}";
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var jsonStream = await response.Content.ReadAsStreamAsync();
                WeatherData data = await JsonSerializer.DeserializeAsync<WeatherData>(jsonStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return data;
            }
            catch (HttpRequestException ex)
            {
                // Consider logging this exception
                Console.WriteLine($"Error (GetWeatherDataAsync - HTTP): {ex.Message}");
                return null;
            }
            catch (JsonException ex)
            {
                // Consider logging this exception
                Console.WriteLine($"Error (GetWeatherDataAsync - JSON): {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                // Consider logging this exception
                Console.WriteLine($"Error (GetWeatherDataAsync - General): {ex.Message}");
                return null;
            }
        }

        public async Task<WeatherOverview?> GetWeatherOverviewAsync()
        {
            string url = $"{ApiOverviewUrl}?lat={_latitude}&lon={_longitude}&units={_units}&appid={_apiKey}";
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var jsonStream = await response.Content.ReadAsStreamAsync();
                WeatherOverview? overviewData = await JsonSerializer.DeserializeAsync<WeatherOverview>(jsonStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return overviewData;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error (GetWeatherOverviewAsync - HTTP): {ex.Message}");
                return null;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error (GetWeatherOverviewAsync - JSON): {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error (GetWeatherOverviewAsync - General): {ex.Message}");
                return null;
            }
        }

        public async Task<LocationData?> GetLocationDataAsync()
        {
            string url = $"{ApiGeoReverseUrl}?lat={_latitude}&lon={_longitude}&limit=1&appid={_apiKey}";
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var jsonStream = await response.Content.ReadAsStreamAsync();
                List<LocationData>? locationList = await JsonSerializer.DeserializeAsync<List<LocationData>>(jsonStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (locationList != null && locationList.Count > 0)
                {
                    return locationList[0];
                }
                return null;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error (GetLocationDataAsync - HTTP): {ex.Message}");
                return null;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error (GetLocationDataAsync - JSON): {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error (GetLocationDataAsync - General): {ex.Message}");
                return null;
            }
        }

        // New method for geocoding lookup
        // This method now uses the same static HttpClient, fixing the error.
        public async Task<List<GeocodingResult>> GetGeocodingDataAsync(string apiKey, string city, string state, string countryCode)
        {
            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(city))
            {
                return new List<GeocodingResult>();
            }
            string geocodingApiUrl = $"https://api.openweathermap.org/geo/1.0/direct?q={city},{state},{countryCode}&limit=10&appid={apiKey}";
            try
            {
                return await httpClient.GetFromJsonAsync<List<GeocodingResult>>(geocodingApiUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error (GetGeocodingDataAsync): {ex.Message}");
                return new List<GeocodingResult>();
            }
        }



        //private async Task RenderHourlyWindChart(List<HourlyWeatherInfo> hourly)
        //{
        //    var labels = hourly.Take(24).Select(h => DateTimeOffset.FromUnixTimeSeconds(h.Dt).LocalDateTime.ToString("h tt")).ToList();
        //    var windSpeeds = hourly.Take(24).Select(h => h.WindSpeed).ToList();
        //    string canvasId = $"hourlyWindChart_{ModuleState.ModuleId}";
        //    var datasets = new List<ChartDataset>
        //                {
        //                    new ChartDataset { Label = "Wind Speed (mph)", Data = windSpeeds, BorderColor = "rgba(255, 99, 132, 1)", BackgroundColor = "rgba(255, 99, 132, 0.2)", Fill = true }
        //                };
        //    await JSRuntime.InvokeVoidAsync("renderChart", canvasId, "line", labels, datasets, "Time", "Wind Speed (mph)", true); // true for yAxisBeginAtZero  
        //}


    }
}
