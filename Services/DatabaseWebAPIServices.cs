using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Energy_Prediction_System.Classes;
using System.Text;
using Microsoft.Maui.Devices; // For DeviceInfo

namespace Energy_Prediction_System.Services
{
    public class DatabaseWebAPIServices
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApiAddress;

        public DatabaseWebAPIServices()
        {
            // Ignorer sertifikatvalidering i utviklingsmodus (for testing)
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true
            };

            _httpClient = new HttpClient(handler);

            // Velg baseadresse basert på plattform
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                _baseApiAddress = "https://10.0.2.2:7107";
            }
            else if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                _baseApiAddress = "https://10.0.2.2:7107";
            }
            else if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                _baseApiAddress = "https://localhost:7107";
            }
            else
            {
                throw new NotSupportedException("Unsupported platform");
            }
        }

        // Generic method to handle API requests with detailed error logging
        private async Task<T> GetAsync<T>(string url)
        {
            var fullUrl = $"{_baseApiAddress}{url}"; // Kombiner baseadresse med URL
            try
            {
                var response = await _httpClient.GetAsync(fullUrl);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResult = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<T>(jsonResult);
                    return result;
                }
                else
                {
                    // Log response details if not successful
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Unable to retrieve data: {response.StatusCode}. Details: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                // Log detailed exception information
                Console.WriteLine($"Exception caught while making GET request to {fullUrl}: {ex.Message}");
                throw;
            }
        }

        // POST requests with detailed error logging
        private async Task<string> PostAsync<T>(string apiUrl, T data)
        {
            var fullUrl = $"{_baseApiAddress}{apiUrl}";
            try
            {
                var jsonData = JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(fullUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync(); // Return API confirmation
                }
                else
                {
                    // Log error details from the response
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Failed to post data: {response.StatusCode}. Details: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                // Log detailed exception information
                Console.WriteLine($"Exception caught while making POST request to {fullUrl}: {ex.Message}");
                throw;
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
        public Task<List<EnergyPredictionItem>> GetLatestEnergyPredictionAsync(string url) => GetAsync<List<EnergyPredictionItem>>(url);
        public Task<List<WeatherForecastItem>> GetWeatherForecastsAsync(string url) => GetAsync<List<WeatherForecastItem>>(url);
        public Task<List<WeatherForecastItem>> GetLatestWeatherForecastsAsync(string url) => GetAsync<List<WeatherForecastItem>>(url);
        public Task<List<WeatherForecastUoMItem>> GetWeatherForecastUoMsAsync(string url) => GetAsync<List<WeatherForecastUoMItem>>(url);

        // Method to get UoM for a specific attribute with error logging
        public async Task<string> GetUoMForAttributeAsync(string url, string attribute)
        {
            var fullUrl = $"{_baseApiAddress}{url}{Uri.EscapeDataString(attribute)}";
            try
            {
                var response = await _httpClient.GetAsync(fullUrl);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    // Log error details
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Unable to retrieve UoM: {response.StatusCode}. Details: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                // Log detailed exception information
                Console.WriteLine($"Exception caught while making UoM request to {fullUrl}: {ex.Message}");
                throw;
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