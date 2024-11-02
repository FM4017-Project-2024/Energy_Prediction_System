using System;

namespace Energy_Prediction_System.Classes
{
    public class WeatherForecastItem
    {
        public int Id { get; set; }
        public string DateTime { get; set; }
        public string ForecastTime { get; set; }
        public float Temperature { get; set; }
        public float WindDirection { get; set; }
        public float WindSpeed { get; set; }
        public float Humidity { get; set; }
        public float Pressure { get; set; }
        public float Cloudiness { get; set; }
        public float LowClouds { get; set; }
        public float MediumClouds { get; set; }
        public float HighClouds { get; set; }
        public float DewpointTemperature { get; set; }
        // public WeatherForecastItem(string dateTime, string forecastTime, float temperature, float windDirection, float windSpeed, float humidity, float pressure, float cloudiness, float lowClouds, float mediumClouds, float highClouds, float dewpointTemperature)
        //{
        //    DateTime = dateTime;
        //    ForecastTime = forecastTime;
        //    Temperature = temperature;
        //    WindDirection = windDirection;
        //    WindSpeed = windSpeed;
        //    Humidity = humidity;
        //    Pressure = pressure;
        //    Cloudiness = cloudiness;
        //    LowClouds = lowClouds;
        //    MediumClouds = mediumClouds;
        //    HighClouds = highClouds;
        //    DewpointTemperature = dewpointTemperature;
        //}
        //public WeatherForecastItem() { }
    }
}
