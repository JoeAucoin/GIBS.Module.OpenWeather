using Oqtane.Models;
using Oqtane.Modules;

namespace GIBS.Module.OpenWeather
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "OpenWeather",
            Description = "Weather data provided by OpenWeatherMap.org",
            Version = "1.0.0",
            ServerManagerType = "GIBS.Module.OpenWeather.Manager.OpenWeatherManager, GIBS.Module.OpenWeather.Server.Oqtane",
            ReleaseVersions = "1.0.3",
            Dependencies = "GIBS.Module.OpenWeather.Shared.Oqtane",
            PackageName = "GIBS.Module.OpenWeather" 

        };
    }
}
