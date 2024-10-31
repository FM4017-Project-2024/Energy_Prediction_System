using System;

namespace Energy_Prediction_System.Classes
{
    public class WeatherForecastItem
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime ForecastTime { get; set; }
        public double Temperature { get; set; }
        public double WindDirection { get; set; }
        public double WindSpeed { get; set; }
        public double Humidity { get; set; }
        public double Pressure { get; set; }
        public double Cloudiness { get; set; }
        public double LowClouds { get; set; }
        public double MediumClouds { get; set; }
        public double HighClouds { get; set; }
        public double DewpointTemperature { get; set; }
    }
}
