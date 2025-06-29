using System.Collections.Generic;
using System.Threading.Tasks;

namespace GIBS.Module.OpenWeather.Services
{
    public interface IOpenWeatherService 
    {
        Task<List<Models.OpenWeather>> GetOpenWeathersAsync(int ModuleId);

        Task<Models.OpenWeather> GetOpenWeatherAsync(int OpenWeatherId, int ModuleId);

        Task<Models.OpenWeather> AddOpenWeatherAsync(Models.OpenWeather OpenWeather);

        Task<Models.OpenWeather> UpdateOpenWeatherAsync(Models.OpenWeather OpenWeather);

        Task DeleteOpenWeatherAsync(int OpenWeatherId, int ModuleId);
    }
}
