using GIBS.Module.OpenWeather.Client.Services;
using GIBS.Module.OpenWeather.Services;
using Microsoft.Extensions.DependencyInjection;
using Oqtane.Services;

namespace GIBS.Module.OpenWeather.Startup
{
    public class ClientStartup : IClientStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IOpenWeatherService, OpenWeatherService>();
            // Add this line to register your WeatherProvider
           // services.AddTransient<WeatherProvider>();
        }
    }
}
