using System;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace Energy_Prediction_System.Classes
{
    public class WeatherService
    {
        private static readonly HttpClient client = new HttpClient();

        public WeatherService()
        {
            // Add the User-Agent header required by the API
            client.DefaultRequestHeaders.Add("User-Agent", "EnergyPredictionSystem/1.0 (242402@usn.no)");
        }
        public async Task<string> GetWeatherAsync(double lat, double lon, double altitude, int index = 0)
        {
            string url = $"https://api.met.no/weatherapi/locationforecast/2.0/classic?lat={lat.ToString(CultureInfo.InvariantCulture)}&lon={lon.ToString(CultureInfo.InvariantCulture)}&altitude={altitude.ToString(CultureInfo.InvariantCulture)}";

            try
            {
                Debug.WriteLine($"Requesting: {url}");

                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return $"Error fetching data: {response.StatusCode} - {errorContent}";
                }

                var responseContent = await response.Content.ReadAsStringAsync();

                // Parse the XML response
                XDocument xmlDoc = XDocument.Parse(responseContent);

                // Access the timeseries data
                var timeseries = xmlDoc.Descendants("time").ToList();

                // Check how many entries are available
                Debug.WriteLine($"Number of time entries available: {timeseries.Count}");

                // If the requested index is out of bounds
                if (timeseries.Count <= index)
                {
                    return $"Error: The requested index ({index}) is out of bounds. Available indices: 0 to {timeseries.Count - 1}.";
                }

                // Get the weather data at the specified index
                var selectedTime = timeseries[index];

                // Log the selected time entry for debugging
                Debug.WriteLine($"Selected Time Entry XML: {selectedTime}");

                // Extract detailed weather data (if available)
                var temperature = selectedTime.Descendants("temperature").FirstOrDefault()?.Attribute("value")?.Value ?? "N/A";
                var windDirection = selectedTime.Descendants("windDirection").FirstOrDefault()?.Attribute("deg")?.Value ?? "N/A";
                var windSpeed = selectedTime.Descendants("windSpeed").FirstOrDefault()?.Attribute("mps")?.Value ?? "N/A";
                var windGust = selectedTime.Descendants("windGust").FirstOrDefault()?.Attribute("mps")?.Value ?? "N/A";
                var humidity = selectedTime.Descendants("humidity").FirstOrDefault()?.Attribute("value")?.Value ?? "N/A";
                var pressure = selectedTime.Descendants("pressure").FirstOrDefault()?.Attribute("value")?.Value ?? "N/A";
                var cloudiness = selectedTime.Descendants("cloudiness").FirstOrDefault()?.Attribute("percent")?.Value ?? "N/A";
                var fog = selectedTime.Descendants("fog").FirstOrDefault()?.Attribute("percent")?.Value ?? "N/A";
                var lowClouds = selectedTime.Descendants("lowClouds").FirstOrDefault()?.Attribute("percent")?.Value ?? "N/A";
                var mediumClouds = selectedTime.Descendants("mediumClouds").FirstOrDefault()?.Attribute("percent")?.Value ?? "N/A";
                var highClouds = selectedTime.Descendants("highClouds").FirstOrDefault()?.Attribute("percent")?.Value ?? "N/A";
                var dewpointTemperature = selectedTime.Descendants("dewpointTemperature").FirstOrDefault()?.Attribute("value")?.Value ?? "N/A";



                return $"Temperature: {temperature}°C, Wind Direction: {windDirection}°, Wind Speed: {windSpeed} m/s, Wind Gust: {windGust} m/s, Humidity: {humidity}%, Pressure: {pressure} hPa, Cloudiness: {cloudiness}%, Fog: {fog}%, Low Clouds: {lowClouds}%, Medium Clouds: {mediumClouds}%, High Clouds: {highClouds}%, Dewpoint Temperature: {dewpointTemperature}°C";

            }
            catch (HttpRequestException httpEx)
            {
                return $"Error fetching data: {httpEx.Message}";
            }
        }
    }
}
