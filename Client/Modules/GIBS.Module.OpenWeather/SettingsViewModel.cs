using Oqtane.Modules;
using Oqtane.Security;
using Oqtane.Services;
using Oqtane.Shared;
using Oqtane.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//namespace GIBS.Module.OpenWeather.Client.Modules.GIBS.Module.OpenWeather
namespace GIBS.Module.OpenWeather
{
    internal class SettingsViewModel
    {
        public static class Settings
        {
            public static string ApiKey => nameof(SettingsViewModel.Settings.ApiKey);
         //   public static string Category => nameof(SettingsViewModel.Topic);
        }

        public SettingsViewModel(ISettingService settingService, Dictionary<string, string> moduleSettings)
        {
            ApiKey = settingService.GetSetting(moduleSettings, "SettingApiKey", "none");

           
        }

        public string ApiKey { get; set; } = "";
     //   public string Category { get; set; } = "General";
    }
}
