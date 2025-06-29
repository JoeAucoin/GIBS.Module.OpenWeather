using System.Collections.Generic;
using System.Threading.Tasks;

namespace GIBS.Module.OpenWeather.Repository
{
    public interface IOpenWeatherRepository
    {
        IEnumerable<Models.OpenWeather> GetOpenWeathers(int ModuleId);
        Models.OpenWeather GetOpenWeather(int OpenWeatherId);
        Models.OpenWeather GetOpenWeather(int OpenWeatherId, bool tracking);
        Models.OpenWeather AddOpenWeather(Models.OpenWeather OpenWeather);
        Models.OpenWeather UpdateOpenWeather(Models.OpenWeather OpenWeather);
        void DeleteOpenWeather(int OpenWeatherId);
    }
}
