using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using Energy_Prediction_System.Classes;
using Energy_Prediction_System.Services;
using Newtonsoft.Json;

namespace Energy_Prediction_System.Classes
{
    public class WeatherService
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly DatabaseWebAPIServices _databaseWebAPIServices;

        public WeatherService()
        {
            // Add the User-Agent header required by the API
            client.DefaultRequestHeaders.Add("User-Agent", "EnergyPredictionSystem/1.0 (242402@usn.no)");
            _databaseWebAPIServices = new DatabaseWebAPIServices();
        }

        public async Task<List<WeatherForecastItem>> GetWeatherDataListAsync(double lat, double lon, double altitude)
        {
            string url = $"https://api.met.no/weatherapi/locationforecast/2.0/classic?lat={lat.ToString(CultureInfo.InvariantCulture)}&lon={lon.ToString(CultureInfo.InvariantCulture)}&altitude={altitude.ToString(CultureInfo.InvariantCulture)}";

            try
            {
                Debug.WriteLine($"Requesting: {url}");

                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error fetching data: {response.StatusCode} - {errorContent}");
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                XDocument xmlDoc = XDocument.Parse(responseContent);
                var timeseries = xmlDoc.Descendants("time").ToList();

                List<WeatherForecastItem> weatherDataList = new List<WeatherForecastItem>();

                foreach (var timeEntry in timeseries)
                {
                    var fromAttribute = timeEntry.Attribute("from")?.Value;
                    var toAttribute = timeEntry.Attribute("to")?.Value;

                    if (fromAttribute == toAttribute)
                    {
                        // Parse each XML entry into a WeatherData instance
                        var weatherData = new WeatherForecastItem(
                        dateTime: DateTime.Now.ToString("o"),
                        forecastTime: timeEntry.Attribute("to")?.Value ?? "",
                        temperature: float.Parse(timeEntry.Descendants("temperature").FirstOrDefault()?.Attribute("value")?.Value ?? "0", CultureInfo.InvariantCulture),
                        windDirection: float.Parse(timeEntry.Descendants("windDirection").FirstOrDefault()?.Attribute("deg")?.Value ?? "0", CultureInfo.InvariantCulture),
                        windSpeed: float.Parse(timeEntry.Descendants("windSpeed").FirstOrDefault()?.Attribute("mps")?.Value ?? "0", CultureInfo.InvariantCulture),
                        humidity: float.Parse(timeEntry.Descendants("humidity").FirstOrDefault()?.Attribute("value")?.Value ?? "0", CultureInfo.InvariantCulture),
                        pressure: float.Parse(timeEntry.Descendants("pressure").FirstOrDefault()?.Attribute("value")?.Value ?? "0", CultureInfo.InvariantCulture),
                        cloudiness: float.Parse(timeEntry.Descendants("cloudiness").FirstOrDefault()?.Attribute("percent")?.Value ?? "0", CultureInfo.InvariantCulture),
                        lowClouds: float.Parse(timeEntry.Descendants("lowClouds").FirstOrDefault()?.Attribute("percent")?.Value ?? "0", CultureInfo.InvariantCulture),
                        mediumClouds: float.Parse(timeEntry.Descendants("mediumClouds").FirstOrDefault()?.Attribute("percent")?.Value ?? "0", CultureInfo.InvariantCulture),
                        highClouds: float.Parse(timeEntry.Descendants("highClouds").FirstOrDefault()?.Attribute("percent")?.Value ?? "0", CultureInfo.InvariantCulture),
                        dewpointTemperature: float.Parse(timeEntry.Descendants("dewpointTemperature").FirstOrDefault()?.Attribute("value")?.Value ?? "0", CultureInfo.InvariantCulture)
                    );
                        weatherDataList.Add(weatherData);
                    }
                }

                return weatherDataList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }




        public async Task<WeatherForecastItem> GetWeatherDataAsync(double lat, double lon, double altitude)
        {
            // Fetch the list of weather data
            List<WeatherForecastItem> weatherDataList = await GetWeatherDataListAsync(lat, lon, altitude);

            // Check if the list has any data
            if (weatherDataList == null || !weatherDataList.Any())
            {
                return null;
            }

            // Find the entry with the lowest forecastTime
            var earliestWeatherData = weatherDataList
                .OrderBy(data => DateTime.Parse(data.ForecastTime, CultureInfo.InvariantCulture))
                .FirstOrDefault();

            return earliestWeatherData;
        }

        public async Task<string> AddWeatherDataToDatabase(double lat, double lon, double altitude)
        {
            try
            {
                // Get the list of WeatherData from the API
                List<WeatherForecastItem> weatherDataList = await GetWeatherDataListAsync(lat, lon, altitude);

                if (weatherDataList == null || !weatherDataList.Any())
                {
                    return "No weather data available to send to the database.";
                }

                // API endpoint for posting weather data
                string apiUrl = "https://localhost:7107/api/WeatherForecastItems"; // Replace with your actual API endpoint
                foreach (WeatherForecastItem weatherData in weatherDataList)
                {
                    // Post each WeatherData instance to the API
                    try
                    {
                        var response = await _databaseWebAPIServices.PostWeatherForecastAsync(apiUrl, weatherData);
                    }
                    catch (Exception ex)
                    {
                        throw;
                        //Debug.WriteLine("Error posting weather data: " + ex.Message);
                    }
                }

                return "Weather data successfully sent to the database.";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error sending weather data to database: {ex.Message}");
                return $"Error: {ex.Message}";
            }
        }
    }
}
