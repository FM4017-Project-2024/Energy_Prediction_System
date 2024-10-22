using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Energy_Prediction_System.Classes;

namespace Energy_Prediction_System.Services
{
    public class DatabaseWebAPIServices
    {
        private readonly HttpClient _httpClient;

        public DatabaseWebAPIServices()
        {
            _httpClient = new HttpClient();
        }

        // Generisk metode for å håndtere API-forespørsler
        private async Task<T> GetAsync<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<T>(jsonResult);
                return result;
            }
            else
            {
                throw new HttpRequestException($"Unable to retrieve data: {response.StatusCode}");
            }
        }

        // Spesifikke metoder som kaller den generiske
        public Task<List<BuildingTemperatureItem>> GetBuildingTempsAsync(string url)
        {
            return GetAsync<List<BuildingTemperatureItem>>(url);
        }

        public Task<BuildingTemperatureItem> GetLatestBuildingTempAsync(string url)
        {
            return GetAsync<BuildingTemperatureItem>(url);
        }

        public Task<List<BuildingRelativeHumidityItem>> GetBuildingRelHumidityAsync(string url)
        {
            return GetAsync<List<BuildingRelativeHumidityItem>>(url);
        }

        public Task<BuildingRelativeHumidityItem> GetLatestBuildingRelHumidityAsync(string url)
        {
            return GetAsync<BuildingRelativeHumidityItem>(url);
        }

        public Task<List<BuildingEnergyMeterItem>> GetBuildingEnergyMeterAsync(string url)
        {
            return GetAsync<List<BuildingEnergyMeterItem>>(url);
        }

        public Task<BuildingEnergyMeterItem> GetLatestBuildingEnergyMeterAsync(string url)
        {
            return GetAsync<BuildingEnergyMeterItem>(url);
        }
    }
}
