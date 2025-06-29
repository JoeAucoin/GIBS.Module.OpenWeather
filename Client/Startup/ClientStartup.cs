using Microsoft.Extensions.DependencyInjection;
using Oqtane.Services;
using GIBS.Module.OpenWeather.Services;

namespace GIBS.Module.OpenWeather.Startup
{
    public class ClientStartup : IClientStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IOpenWeatherService, OpenWeatherService>();
        }
    }
}
