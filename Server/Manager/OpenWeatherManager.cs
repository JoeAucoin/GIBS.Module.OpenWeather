using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Oqtane.Modules;
using Oqtane.Models;
using Oqtane.Infrastructure;
using Oqtane.Interfaces;
using Oqtane.Enums;
using Oqtane.Repository;
using GIBS.Module.OpenWeather.Repository;
using System.Threading.Tasks;

namespace GIBS.Module.OpenWeather.Manager
{
    public class OpenWeatherManager : MigratableModuleBase, IInstallable, IPortable, ISearchable
    {
        private readonly IOpenWeatherRepository _OpenWeatherRepository;
        private readonly IDBContextDependencies _DBContextDependencies;

        public OpenWeatherManager(IOpenWeatherRepository OpenWeatherRepository, IDBContextDependencies DBContextDependencies)
        {
            _OpenWeatherRepository = OpenWeatherRepository;
            _DBContextDependencies = DBContextDependencies;
        }

        public bool Install(Tenant tenant, string version)
        {
            return Migrate(new OpenWeatherContext(_DBContextDependencies), tenant, MigrationType.Up);
        }

        public bool Uninstall(Tenant tenant)
        {
            return Migrate(new OpenWeatherContext(_DBContextDependencies), tenant, MigrationType.Down);
        }

        public string ExportModule(Oqtane.Models.Module module)
        {
            string content = "";
            List<Models.OpenWeather> OpenWeathers = _OpenWeatherRepository.GetOpenWeathers(module.ModuleId).ToList();
            if (OpenWeathers != null)
            {
                content = JsonSerializer.Serialize(OpenWeathers);
            }
            return content;
        }

        public void ImportModule(Oqtane.Models.Module module, string content, string version)
        {
            List<Models.OpenWeather> OpenWeathers = null;
            if (!string.IsNullOrEmpty(content))
            {
                OpenWeathers = JsonSerializer.Deserialize<List<Models.OpenWeather>>(content);
            }
            if (OpenWeathers != null)
            {
                foreach(var OpenWeather in OpenWeathers)
                {
                    _OpenWeatherRepository.AddOpenWeather(new Models.OpenWeather { ModuleId = module.ModuleId, Name = OpenWeather.Name });
                }
            }
        }

        public Task<List<SearchContent>> GetSearchContentsAsync(PageModule pageModule, DateTime lastIndexedOn)
        {
           var searchContentList = new List<SearchContent>();

           foreach (var OpenWeather in _OpenWeatherRepository.GetOpenWeathers(pageModule.ModuleId))
           {
               if (OpenWeather.ModifiedOn >= lastIndexedOn)
               {
                   searchContentList.Add(new SearchContent
                   {
                       EntityName = "GIBSOpenWeather",
                       EntityId = OpenWeather.OpenWeatherId.ToString(),
                       Title = OpenWeather.Name,
                       Body = OpenWeather.Name,
                       ContentModifiedBy = OpenWeather.ModifiedBy,
                       ContentModifiedOn = OpenWeather.ModifiedOn
                   });
               }
           }

           return Task.FromResult(searchContentList);
        }
    }
}
