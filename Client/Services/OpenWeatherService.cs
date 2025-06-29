using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Oqtane.Services;
using Oqtane.Shared;

namespace GIBS.Module.OpenWeather.Services
{
    public class OpenWeatherService : ServiceBase, IOpenWeatherService
    {
        public OpenWeatherService(HttpClient http, SiteState siteState) : base(http, siteState) { }

        private string Apiurl => CreateApiUrl("OpenWeather");

        public async Task<List<Models.OpenWeather>> GetOpenWeathersAsync(int ModuleId)
        {
            List<Models.OpenWeather> OpenWeathers = await GetJsonAsync<List<Models.OpenWeather>>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={ModuleId}", EntityNames.Module, ModuleId), Enumerable.Empty<Models.OpenWeather>().ToList());
            return OpenWeathers.OrderBy(item => item.Name).ToList();
        }

        public async Task<Models.OpenWeather> GetOpenWeatherAsync(int OpenWeatherId, int ModuleId)
        {
            return await GetJsonAsync<Models.OpenWeather>(CreateAuthorizationPolicyUrl($"{Apiurl}/{OpenWeatherId}/{ModuleId}", EntityNames.Module, ModuleId));
        }

        public async Task<Models.OpenWeather> AddOpenWeatherAsync(Models.OpenWeather OpenWeather)
        {
            return await PostJsonAsync<Models.OpenWeather>(CreateAuthorizationPolicyUrl($"{Apiurl}", EntityNames.Module, OpenWeather.ModuleId), OpenWeather);
        }

        public async Task<Models.OpenWeather> UpdateOpenWeatherAsync(Models.OpenWeather OpenWeather)
        {
            return await PutJsonAsync<Models.OpenWeather>(CreateAuthorizationPolicyUrl($"{Apiurl}/{OpenWeather.OpenWeatherId}", EntityNames.Module, OpenWeather.ModuleId), OpenWeather);
        }

        public async Task DeleteOpenWeatherAsync(int OpenWeatherId, int ModuleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{OpenWeatherId}/{ModuleId}", EntityNames.Module, ModuleId));
        }
    }
}
