using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Energy_Prediction_System.Classes;
using System.Text;

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

        // Generisk metode for POST-forespørsler med en dynamisk URL
        private async Task<string> PostAsync<T>(string apiUrl, T data)
        {
            var jsonData = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync(); // Returner API-bekreftelse
            }
            else
            {
                throw new HttpRequestException($"Failed to post data: {response.StatusCode}");
            }
        }

        // POST-metoder mottar nå URL som parameter
        public Task<string> PostBuildingTemperatureAsync(string apiUrl, BuildingTemperatureItem item)
        {
            return PostAsync(apiUrl, item);
        }

        public Task<string> PostBuildingRelativeHumidityAsync(string apiUrl, BuildingRelativeHumidityItem item)
        {
            return PostAsync(apiUrl, item);
        }

        public Task<string> PostBuildingEnergyMeterAsync(string apiUrl, BuildingEnergyMeterItem item)
        {
            return PostAsync(apiUrl, item);
        }
    }
}
