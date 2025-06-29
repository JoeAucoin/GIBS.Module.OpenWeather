using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GIBS.Module.OpenWeather.Client.Services
{
    internal class LocationData
    {
        public string name { get; set; }
        public LocalNames local_names { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string country { get; set; }
        public string state { get; set; }
    }

    public class LocalNames
    {
        // Corrected attribute to use JsonPropertyName instead of JsonProperty  
        [JsonPropertyName("en")]
        public string En { get; set; }
        // Example for another language:  
        // [JsonPropertyName("fr")]  
        // public string Fr { get; set; }  
    }
}
