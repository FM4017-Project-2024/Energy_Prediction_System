using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using Microsoft.Maui.Storage;
using Microsoft.Extensions.Configuration;
using Energy_Prediction_System.Classes;
using System.Data;
using Newtonsoft.Json.Linq;

namespace Energy_Prediction_System.Services
{
    public class PredictionService
    {
        private readonly string? apiKey;
        private readonly DatabaseWebAPIServices _databaseWebAPIServices;

        public PredictionService()
        {
            _databaseWebAPIServices = new DatabaseWebAPIServices();
            apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY_Project24");

            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("API key not set in environment variables. Read the readme file for guideance.");
            }
        }

        private static readonly string endpoint = "https://24240-m1ksp369-westeurope.openai.azure.com/openai/deployments/gpt-35-turbo/chat/completions?api-version=2024-05-01-preview";

        public async Task<string> GetEnergyConsumptionPredictionAsync(List<DateTime> days)
        {
            /*
                Method that returns predicted energy consumption for a given input day
                Day must be within range of api (typically 10 days ahead)
             */


            string promptData = await GetAPITrainingData();
            StringBuilder predictData = new StringBuilder();
            foreach (var day in days) { predictData.AppendLine(await GetInputDataForPrediction(day)); }
            //string predictData = await GetInputDataForPrediction(day);

            // Prepare the JSON payload for the request
            var data = new
            {
                messages = new[]
                {
                    new { role = "system", content = "You are an AI model that strictly provides JSON-formatted predictions based on provided data, with no additional text or explanations." },
                    new
                    {
                        role = "user",
                        content = $@"
                            Predict the daily energy consumption for the following days based on the following conditions:
                            {predictData}

                            Based on the historical training data:
                            {promptData}

                            Respond only in JSON format with an array of predictions for each day, where each entry includes the date and the predicted energy consumption. Use the following exact format and do not include any other text:

                            {{
                                ""predictions"": [
                                    {{ ""date"": ""YYYY-MM-DD"", ""consumption"": predicted_value }},
                                    {{ ""date"": ""YYYY-MM-DD"", ""consumption"": predicted_value }},
                                    ...
                                ]
                            }}
                            Note: Introduce minor differences in consumption values each day, within a realistic range."

                        //content = $@"
                        //Predict the energy consumption for the following conditions:
                        //{predictData}

                        //Based on the historical training data:
                        //{promptData}

                        //Respond with only the predicted energy consumption as a single number without any units, explanation, or extra text. Format: just the number (e.g., '123')."
                    }
                },
                max_tokens = 800,
                temperature = 0.7,
                top_p = 0.95,
                frequency_penalty = 0,
                presence_penalty = 0
            };

            // Create the HTTP request
            var client = new RestClient(endpoint);
            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("api-key", apiKey);
            request.AddJsonBody(data);

            // Execute the request and process the response
            var response = await client.ExecutePostAsync(request);
            if (response.IsSuccessful)
            {
                var result = JsonConvert.DeserializeObject<dynamic>(response.Content);
                string assistantResponse = result.choices[0].message.content;
                return assistantResponse;
            }
            else
            {
                throw new Exception($"API call failed with status code: {response.StatusCode}\n{response.Content}");
            }
        }
        public async Task<bool> IsDatabaseUpdatedAsync()
        {
            /*
                Method that checks if the prediction api has been run today
             */
            DateTime today = DateTime.Now;
            EnergyPredictionItem energyPredictionItem;
            string apiUrl = "/api/EnergyPredictionItems/latest";
            try
            {
                energyPredictionItem = await _databaseWebAPIServices.GetLatestEnergyPredictionAsync(apiUrl);
                // Check if the ExecuteTime date matches today's date (ignoring time)
                if (energyPredictionItem != null && energyPredictionItem.ExecuteTime.Date == today.Date)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }


            return false;
        }
        public async void UpdateDatabaseWithPredictions(int numberOfDays)
        {
            DateTime today = DateTime.Now.Date;
            var predictionDays = Enumerable.Range(0, numberOfDays)
                          .Select(offset => today.AddDays(offset))
                          .ToList();
            string jsonPredictions = await GetEnergyConsumptionPredictionAsync(predictionDays);
            try
            {
                // Attempt to parse response as JSON to ignore non-JSON text
                var jsonResponse = JObject.Parse(jsonPredictions);
                if (jsonResponse["predictions"] is JArray predictionsArray)
                {
                    foreach (var prediction in predictionsArray)
                    {
                        // Parse date and consumption from each prediction entry
                        DateTime day = DateTime.Parse(prediction["date"].ToString());
                        float consumption = float.Parse(prediction["consumption"].ToString());

                        // Call the method to push data to the database
                        PushPredictedToDatabase(day, consumption);
                    }
                }
            }
            catch (JsonException)
            {
                throw;
            }

        }
        private async void PushPredictedToDatabase(DateTime day, float consumption)
        {
            /*
                Method that uploads predicted energy consumption to database
             */

            string apiUrl = "/api/EnergyPredictionItems";

            var energyPredictionItem = new EnergyPredictionItem
            {
                EnergyPrediction = consumption,
                EnergyPredictionUoM = "°C",
                DateTime = day,
                ExecuteTime = DateTime.Now
            };

            try
            {
                var response = await _databaseWebAPIServices.PostEnergyPredictionAsync(apiUrl, energyPredictionItem);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string ConstructAPIString(List<WeatherForecastItem> weatherForecastList, List<BuildingEnergyMeterItem> buildingEnergyMeterList)
        {
            /*
                Method that returns a string with date, temperature and energy consumption on the same line
             */


            // Use StringBuilder to construct the input string
            StringBuilder promptData = new StringBuilder();

            foreach (var forecast in weatherForecastList)
            {
                // Try to parse ForecastTime as DateTime
                if (DateTime.TryParse(forecast.ForecastTime, out DateTime forecastDate))
                {
                    // Find the first energy meter entry with the same day
                    var matchingEnergyMeter = buildingEnergyMeterList
                        .FirstOrDefault(meter => meter.EnergyMeterDateTime.Date == forecastDate.Date);

                    // If a matching energy meter entry is found, add the data to the string
                    if (matchingEnergyMeter != null)
                    {
                        promptData.AppendLine($"Date: {forecastDate:yyyy-MM-dd}, Temperature: {forecast.Temperature}°C, Energy Consumption: {matchingEnergyMeter.EnergyMeter1} kWh");
                    }
                }
                else
                {
                    // Handle parsing failure if necessary (optional)
                    Console.WriteLine($"Warning: Could not parse ForecastTime '{forecast.ForecastTime}'");
                }
            }

            return promptData.ToString();
        }

        //// Define a class to map weather data
        //public class WeatherEnergyData
        //{
        //    public string Date { get; set; }
        //    public int Temperature { get; set; }
        //    public int Humidity { get; set; }
        //    public int WindSpeed { get; set; }
        //    public int EnergyConsumption { get; set; }
        //}


        public async Task<string> GetAPITrainingData()
        {
            /*
                Method that returns a string with training data for all available energy consumptions in database
             */

            string weatherApiUrl = "/api/WeatherForecastItems";
            string energyApiUrl = "/api/BuildingEnergyMeterItems";
            string apiString = "";
            List<WeatherForecastItem> weatherForecastList;
            List<BuildingEnergyMeterItem> buildingEnergyMeterList;

            try
            {
                weatherForecastList = await _databaseWebAPIServices.GetWeatherForecastsAsync(weatherApiUrl);
                buildingEnergyMeterList = await _databaseWebAPIServices.GetBuildingEnergyMeterAsync(energyApiUrl);
            }
            catch (Exception)
            {
                throw;
            }
            apiString = ConstructAPIString(weatherForecastList, buildingEnergyMeterList);
            return apiString;
        }

        public async Task<string> GetInputDataForPrediction(DateTime day)
        {
            /*
                Method that returns average temperature for given input day
             */

            List<WeatherForecastItem> weatherForecastList;
            string weatherApiUrl = "/api/WeatherForecastItems";
            try
            {
                weatherForecastList = await _databaseWebAPIServices.GetWeatherForecastsAsync(weatherApiUrl);
            }
            catch (Exception)
            {
                throw;
            }

            // Filter records where the date matches the specified day
            var matchingRecords = weatherForecastList
                .Where(record => DateTime.TryParse(record.ForecastTime, out DateTime forecastDate) && forecastDate.Date == day.Date)
                .ToList();

            // Check if there are any matching records
            if (!matchingRecords.Any())
            {
                return $"No data available for {day:yyyy-MM-dd}.";
            }

            // Calculate the average temperature
            double averageTemperature = matchingRecords.Average(record => record.Temperature);

            // Return the formatted result
            return $"Date: {day:yyyy-MM-dd}, Average Temperature: {averageTemperature:F2}°C";
        }
    }
}

