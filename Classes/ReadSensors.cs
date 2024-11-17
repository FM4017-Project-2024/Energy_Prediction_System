using Energy_Prediction_System.Services;
using Energy_Prediction_System.Classes;
using System.Diagnostics;


namespace Energy_Prediction_System.Classes
{
    public class ReadSensors
    {
        public Livesensors sensor { get; set; } = new Livesensors();
        public HistoricData historical { get; set; } = new HistoricData();
        public Historic_weather historicalWeather { get; set; } = new Historic_weather();

        private readonly DatabaseWebAPIServices _databaseWebAPIServices;

        public ReadSensors()
        {
            _databaseWebAPIServices = new DatabaseWebAPIServices();
            getSensorData();
        }

        public class Livesensors
        {
            public double TT01 { get; set; }
            public double TT02 { get; set; }
            public double TT03 { get; set; }
            public double TT04 { get; set; }
            public double TT05 { get; set; }
            public double TT_AVG { get; set; }
            public DateTime? TT_DT { get; set; }
            public double RHT01 { get; set; }
            public double RHT02 { get; set; }
            public double RHT03 { get; set; }
            public double RHT04 { get; set; }
            public double RHT_AVG { get; set; }
            public DateTime? RHT_DT { get; set; }
            public double KWH01 { get; set; }
            public DateTime? KWH_DT { get; set; }
            public float Current_Prediciton { get; set; }
        }
        public class HistoricData
        {
            public float[] TT01 { get; set; }
            public float[] TT02 { get; set; }
            public float[] TT03 { get; set; }
            public float[] TT04 { get; set; }
            public float[] TT05 { get; set; }
            public float[] RHT01 { get; set; }
            public float[] RHT02 { get; set; }
            public float[] RHT03 { get; set; }
            public float[] RHT04 { get; set; }
            public float[] KWH01 { get; set; }
        }
        public class Historic_weather
        {
            public float[] Temperature { get; set; }
            public float[] WindDirection { get; set; }
            public float[] WindSpeed { get; set; }
            public float[] Humidity { get; set; }
            public float[] Pressure { get; set; }
            public float[] Cloudiness { get; set; }
            public float[] LowClouds { get; set; }
            public float[] MediumClouds { get; set; }
            public float[] HighClouds { get; set; }
            public float[] DewpointTemperature { get; set; }
            public DateTime[] DT { get; set; }
        }

        public void getSensorData()
        {
            GetLatestBuildingTemp();
            GetLatestBuildingRelHumidity();
            GetLatestEnergyMeterData();
            GetAllBuildingTemp();
            GetAllBuildingRelHum();
            GetAllBuildingKwh();
            GetAllWeatherForecasts();
            GetLatestEnergyPrediction();
        }

        // Methods to retrieve weather forecast data
        private async void GetAllWeatherForecasts()
        {
            string apiUrl = "/api/WeatherForecastItems";
            try
            {
                List<WeatherForecastItem> weatherForecastItems = await _databaseWebAPIServices.GetWeatherForecastsAsync(apiUrl);
                // Last 14 days
                List<WeatherForecastItem> last14Items = weatherForecastItems.OrderByDescending(item => item.Id).Take(14).ToList();
                historicalWeather.Temperature = last14Items.Select(item => item.Temperature).ToArray();
                historicalWeather.WindDirection = last14Items.Select(item => item.WindDirection).ToArray();
                historicalWeather.WindSpeed = last14Items.Select(item => item.WindSpeed).ToArray();
                historicalWeather.Humidity = last14Items.Select(item => item.Humidity).ToArray();
                historicalWeather.Pressure = last14Items.Select(item => item.Pressure).ToArray();
                historicalWeather.Cloudiness = last14Items.Select(item => item.Cloudiness).ToArray();
                historicalWeather.LowClouds = last14Items.Select(item => item.LowClouds).ToArray();
                historicalWeather.MediumClouds = last14Items.Select(item => item.MediumClouds).ToArray();
                historicalWeather.HighClouds = last14Items.Select(item => item.HighClouds).ToArray();
                historicalWeather.DewpointTemperature = last14Items.Select(item => item.DewpointTemperature).ToArray();
                historicalWeather.DT = last14Items.Select(item => DateTime.Parse(item.DateTime)).ToArray();

                foreach (var item in last14Items)
                {
                    Debug.WriteLine(item.DateTime); // Assuming DateTime is the property name in WeatherForecastItem
                }




            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex + " Failed to fetch weather data");
            }
        }

        private async void GetLatestEnergyPrediction()
        {
            string apiUrl = "/api/EnergyPredictionItems/latest";
            try
            {
                List<EnergyPredictionItem> latestPrediction = await _databaseWebAPIServices.GetLatestEnergyPredictionAsync(apiUrl);
                EnergyPredictionItem newestPrediction = latestPrediction.OrderByDescending(prediction => prediction.DateTime).FirstOrDefault();
                sensor.Current_Prediciton = newestPrediction.EnergyPrediction;
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex + " Failed to fetch latest energy prediction");
            }
        }



        private async void GetAllBuildingKwh()
        {
            string apiUrl = "/api/BuildingEnergyMeterItems";
            try
            {
                List<BuildingEnergyMeterItem> kwhItems = await _databaseWebAPIServices.GetBuildingEnergyMeterAsync(apiUrl);
                // Last 14 days
                List<BuildingEnergyMeterItem> last14Items = kwhItems.OrderByDescending(item => item.Id).Take(14).ToList();
                historical.KWH01 = last14Items.Select(item => item.EnergyMeter1).ToArray();
                foreach (var item in last14Items)
                {
                    Debug.WriteLine(item.EnergyMeterDateTime); // Assuming DateTime is the property name in WeatherForecastItem
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex + " Failed to fetch kwh data");
            }
        }

        private async void GetAllBuildingRelHum()
        {
            string apiUrl = "/api/BuildingRelativeHumidityItems";
            try
            {
                List<BuildingRelativeHumidityItem> relHumItems = await _databaseWebAPIServices.GetBuildingRelHumidityAsync(apiUrl);
                // Last 14 days
                List<BuildingRelativeHumidityItem> last14Items = relHumItems.OrderByDescending(item => item.Id).Take(14).ToList();
                historical.RHT01 = last14Items.Select(item => item.RelHumidity1).ToArray();
                historical.RHT02 = last14Items.Select(item => item.RelHumidity1).ToArray();
                historical.RHT03 = last14Items.Select(item => item.RelHumidity1).ToArray();
                historical.RHT04 = last14Items.Select(item => item.RelHumidity1).ToArray();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex + " Failed to fetch rel hum data");
            }
        }

        private async void GetAllBuildingTemp()
        {
            string apiUrl = "/api/BuildingTemperatureItems";
            try
            {
                List<BuildingTemperatureItem> temperatureItems = await _databaseWebAPIServices.GetBuildingTempsAsync(apiUrl);
                // Last 14 days
                List<BuildingTemperatureItem> last14Items = temperatureItems.OrderByDescending(item => item.Id).Take(14).ToList();
                historical.TT01 = last14Items.Select(item => item.Temp1).ToArray();
                historical.TT02 = last14Items.Select(item => item.Temp2).ToArray();
                historical.TT03 = last14Items.Select(item => item.Temp3).ToArray();
                historical.TT04 = last14Items.Select(item => item.Temp4).ToArray();
                historical.TT05 = last14Items.Select(item => item.Temp5).ToArray();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex + " Failed to fetch temperature data");
            }
        }

        // Method to retrieve temperature data
        private async void GetLatestBuildingTemp()
        {
            string apiUrl = "/api/BuildingTemperatureItems/latest";
            try
            {
                var latestTemp = await _databaseWebAPIServices.GetLatestBuildingTempAsync(apiUrl);
                sensor.TT01 = latestTemp.Temp1;
                sensor.TT02 = latestTemp.Temp2;
                sensor.TT03 = latestTemp.Temp3;
                sensor.TT04 = latestTemp.Temp4;
                sensor.TT05 = latestTemp.Temp5;
                sensor.TT_AVG = (latestTemp.Temp1 + latestTemp.Temp2 + latestTemp.Temp3 + latestTemp.Temp4 + latestTemp.Temp5) / 5.0;
                sensor.TT_DT = latestTemp.TempDateTime;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex + " Failed to fetch temperature data");
                //await HandleError(ex, "Failed to fetch temperature data");
            }
        }

        private async void GetLatestBuildingRelHumidity()
        {
            string apiUrl = "/api/BuildingRelativeHumidityItems/latest";
            try
            {
                var latestHumidity = await _databaseWebAPIServices.GetLatestBuildingRelHumidityAsync(apiUrl);
                sensor.RHT01 = latestHumidity.RelHumidity1;
                sensor.RHT02 = latestHumidity.RelHumidity2; 
                sensor.RHT03 = latestHumidity.RelHumidity3;
                sensor.RHT04 = latestHumidity.RelHumidity4;
                sensor.RHT_AVG = (latestHumidity.RelHumidity1 + latestHumidity.RelHumidity2 + latestHumidity.RelHumidity3 + latestHumidity.RelHumidity4) / 4.0;
                sensor.RHT_DT = latestHumidity.RelHumidityDateTime;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex + " Failed to fetch latest humidity");
                //await HandleError(ex, "Failed to fetch latest humidity");
            }
        }

        private async void GetLatestEnergyMeterData()
        {
            string apiUrl = "/api/BuildingEnergyMeterItems/latest";
            try
            {
                var latestEnergyMeter = await _databaseWebAPIServices.GetLatestBuildingEnergyMeterAsync(apiUrl);
                sensor.KWH01 = latestEnergyMeter.EnergyMeter1;
                sensor.KWH_DT = latestEnergyMeter.EnergyMeterDateTime;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex + " Failed to fetch latest energy meter data");
                //await HandleError(ex, "Failed to fetch latest energy meter data");
            }
        }

        /*
        // Error handling method
        private async Task HandleError(Exception ex, string message)
        {
            await DisplayAlert("Error", $"{message}: {ex.Message}", "OK");
        }
        */
    }
}

