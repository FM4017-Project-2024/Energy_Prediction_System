using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Energy_Prediction_System.Classes
{
    public class ReadWeatherData
    {
        public WeatherData weather { get; set; } = new WeatherData();
        private readonly WeatherService _weatherService = new();
        private readonly SensorSim _rndVal = new();

        private readonly bool _simulate = false;

        public ReadWeatherData()
        {
    
        }
        public class WeatherData
        {
            public string? TTT { get; set; }
            public string? dd { get; set; }
            public string? ff { get; set; }
            public string? NA { get; set; }
            public string? pr { get; set; }
            public string? NN { get; set; }
            public string? LOW { get; set; }
            public string? MEDIUM { get; set; }
            public string? HIGH { get; set; }
            public string? TD { get; set; }
            public DateTime? currentDateTime { get; set; }
        }

        public void getWeatherData()
        {
 
            if (_simulate)
            {
                StartSimulatingSensorData();
            }
            else
            {
                ReadWeatherDataAPI();
            }
        }

        private void StartSimulatingSensorData()
        {
            weather.TTT = _rndVal.GetRandomDouble(0, 20).ToString();
            weather.dd = _rndVal.GetRandomDouble(0, 10).ToString();
            weather.ff = _rndVal.GetRandomDouble(0, 30).ToString();
            weather.NA = _rndVal.GetRandomDouble(0, 30).ToString();
            weather.pr = _rndVal.GetRandomDouble(0, 30).ToString();
            weather.NN = _rndVal.GetRandomDouble(0, 30).ToString();
            weather.LOW = _rndVal.GetRandomDouble(0, 30).ToString();
            weather.MEDIUM = _rndVal.GetRandomDouble(0, 30).ToString();
            weather.HIGH = _rndVal.GetRandomDouble(0, 30).ToString();
            weather.TD = _rndVal.GetRandomDouble(0, 30).ToString();
            weather.currentDateTime = DateTime.Now;
        }

        private async void ReadWeatherDataAPI()
        {
            WeatherForecastItem weatherData = await _weatherService.GetWeatherDataAsync(59.7076562, 10.1559495, 200);

            weather.currentDateTime = DateTime.Now;
            weather.TTT = weatherData.Temperature.ToString();
            weather.dd = weatherData.WindDirection.ToString();
            weather.ff = weatherData.WindSpeed.ToString();
            weather.NA = weatherData.Humidity.ToString();
            weather.pr = weatherData.Pressure.ToString();
            weather.NN = weatherData.Cloudiness.ToString();
            weather.LOW = weatherData.LowClouds.ToString();
            weather.MEDIUM = weatherData.MediumClouds.ToString();
            weather.HIGH = weatherData.HighClouds.ToString();
            weather.TD = weatherData.DewpointTemperature.ToString();

            Debug.WriteLine($"TTT: {weather.TTT}");
            Debug.WriteLine($"dd: {weather.dd}");
            Debug.WriteLine($"ff: {weather.ff}");
            Debug.WriteLine($"NA: {weather.NA}");
            Debug.WriteLine($"pr: {weather.pr}");
            Debug.WriteLine($"NN: {weather.NN}");
            Debug.WriteLine($"LOW: {weather.LOW}");
            Debug.WriteLine($"MEDIUM: {weather.MEDIUM}");
            Debug.WriteLine($"HIGH: {weather.HIGH}");
            Debug.WriteLine($"TD: {weather.TD}");
            Debug.WriteLine($"DateTime: {weather.currentDateTime}");

        }
    }
}