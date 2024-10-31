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

        // Generic method to handle API requests
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

        // Specific methods calling the generic one for classes
        public Task<List<BuildingTemperatureItem>> GetBuildingTempsAsync(string url) => GetAsync<List<BuildingTemperatureItem>>(url);
        public Task<BuildingTemperatureItem> GetLatestBuildingTempAsync(string url) => GetAsync<BuildingTemperatureItem>(url);
        public Task<List<BuildingRelativeHumidityItem>> GetBuildingRelHumidityAsync(string url) => GetAsync<List<BuildingRelativeHumidityItem>>(url);
        public Task<BuildingRelativeHumidityItem> GetLatestBuildingRelHumidityAsync(string url) => GetAsync<BuildingRelativeHumidityItem>(url);
        public Task<List<BuildingEnergyMeterItem>> GetBuildingEnergyMeterAsync(string url) => GetAsync<List<BuildingEnergyMeterItem>>(url);
        public Task<BuildingEnergyMeterItem> GetLatestBuildingEnergyMeterAsync(string url) => GetAsync<BuildingEnergyMeterItem>(url);
        public Task<List<EnergyPredictionItem>> GetEnergyPredictionsAsync(string url) => GetAsync<List<EnergyPredictionItem>>(url);
        public Task<EnergyPredictionItem> GetLatestEnergyPredictionAsync(string url) => GetAsync<EnergyPredictionItem>(url);
        public Task<List<WeatherForecastItem>> GetWeatherForecastsAsync(string url) => GetAsync<List<WeatherForecastItem>>(url);
        public Task<List<WeatherForecastItem>> GetLatestWeatherForecastsAsync(string url) => GetAsync<List<WeatherForecastItem>>(url);
        public Task<List<WeatherForecastUoMItem>> GetWeatherForecastUoMsAsync(string url) => GetAsync<List<WeatherForecastUoMItem>>(url);

        // Method to get UoM for a specific attribute
        public async Task<string> GetUoMForAttributeAsync(string apiUrl, string attribute)
        {
            var urlWithAttribute = $"{apiUrl}/uom/{Uri.EscapeDataString(attribute)}";
            var response = await _httpClient.GetAsync(urlWithAttribute);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync(); // Returns plain text, e.g., "deg C"
            }
            else
            {
                throw new HttpRequestException($"Unable to retrieve UoM: {response.StatusCode}");
            }
        }

        // Generic method for POST requests with a dynamic URL
        private async Task<string> PostAsync<T>(string apiUrl, T data)
        {
            var jsonData = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync(); // Return API confirmation
            }
            else
            {
                throw new HttpRequestException($"Failed to post data: {response.StatusCode}");
            }
        }

        // POST methods for classes
        public Task<string> PostBuildingTemperatureAsync(string apiUrl, BuildingTemperatureItem item) => PostAsync(apiUrl, item);
        public Task<string> PostBuildingRelativeHumidityAsync(string apiUrl, BuildingRelativeHumidityItem item) => PostAsync(apiUrl, item);
        public Task<string> PostBuildingEnergyMeterAsync(string apiUrl, BuildingEnergyMeterItem item) => PostAsync(apiUrl, item);
        public Task<string> PostEnergyPredictionAsync(string apiUrl, EnergyPredictionItem item) => PostAsync(apiUrl, item);
        public Task<string> PostWeatherForecastAsync(string apiUrl, WeatherForecastItem item) => PostAsync(apiUrl, item);
        public Task<string> PostWeatherForecastUoMAsync(string apiUrl, WeatherForecastUoMItem item) => PostAsync(apiUrl, item);
    }
}
