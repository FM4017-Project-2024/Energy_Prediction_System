using Syncfusion.Maui.Core.Hosting;
using System;
using System.Threading.Tasks;
using Energy_Prediction_System.Services;
using Energy_Prediction_System.Classes;
using Microsoft.Maui.Controls;
using System.Diagnostics;

namespace Energy_Prediction_System
{
    public partial class App : Application
    {
        private readonly WeatherService _weatherService = new(); // Start weather service
        public static ReadSensors SensorValues { get; set; } = new ReadSensors();
        public static ReadWeatherData WeatherValues { get; set; } = new ReadWeatherData();


        private bool _isRunning = true; // Variable to check if application is running
        private DateTime _lastUpdate = DateTime.MinValue; // Variable for last update
        public App()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NDaF5cWWtCf1NpR2NGfV5ycEVDal9YTndcUiweQnxTdEFiWX5dcHFWT2FfWER3Xg==");
            MainPage = new NavigationPage(new MainPage());

            
            StartReadWeatherDataBackgroundTask();
            StartWeatherDataBackgroundTask();
            StartReadSensorDataBackgroundTask();
        }
        // Background taks to read sensor data from API
        private void StartReadWeatherDataBackgroundTask()
        {
            Task.Run(async () =>
            {
                while (_isRunning)
                {
                    // Call the function to get sensor data
                    WeatherValues.getWeatherData();

                    // Wait for 30 seconds before the next execution
                    await Task.Delay(TimeSpan.FromMinutes(60));
                }
            });
        }
        // Background taks to read sensor data from API
        private void StartReadSensorDataBackgroundTask()
        {
            Task.Run(async () =>
            {
                while (_isRunning)
                {
                    SensorValues.getSensorData();

                    // Wait for 30 seconds before the next execution
                    await Task.Delay(TimeSpan.FromSeconds(60));
                }
            });
        }

        // Background taks to update weather data in database every hour
        private void StartWeatherDataBackgroundTask()
        {
            Task.Run(async () =>
            {
                // Check if application is running
                while (_isRunning)
                {
                    // Check if there is one hour from latest update
                    if ((DateTime.Now - _lastUpdate).TotalHours >= 1)
                    {
                        // Post data to database
                        await _weatherService.AddWeatherDataToDatabase(59.7076562, 10.1559495, 90);
                        _lastUpdate = DateTime.Now;
                    }
                    await Task.Delay(TimeSpan.FromMinutes(1)); // Will check every minute
                }
            });
        }

        protected override void OnStart()
        {
            _isRunning = true; // Set running variable when starting the application
        }

    }

}
