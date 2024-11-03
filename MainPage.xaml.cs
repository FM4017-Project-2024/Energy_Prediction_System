using Microsoft.Maui.Controls;
using Energy_Prediction_System.Views;
using Energy_Prediction_System.Services;
using Energy_Prediction_System.Classes;
using System.Runtime.CompilerServices;

namespace Energy_Prediction_System
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLiveSensorData1ButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new LiveSensorData());
        private async void OnLiveWeatherData1ButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new LiveWheaterData());
        private async void OnDatabaseViewPageButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new DatabaseViewPage());
        private async void OnOpenDatabasePostPageClicked(object sender, EventArgs e) => await Navigation.PushAsync(new DatabasePostPage());


        //public async void UpdateWeatherDatabaseAsync()
        //{
        //    try
        //    {
        //        private readonly DatabaseWebAPIServices _webAPIService = new();
        //        private readonly WeatherService _weatherService = new();
        //        string databaseWeatherDataUrl = "https://localhost:7107/api/WeatherForecastItems/latest";

        //        WeatherForecastItem latestForecast = await _webAPIService.GetLatestWeatherForecastsAsync(databaseWeatherDataUrl);

        //        // Get the latest weather data entry from the database
        //        List<WeatherForecastItem> databaseWeatherDataList = await GetLatestWeatherForecastsAsync(databaseWeatherDataUrl);
        //        WeatherForecastItem latestDatabaseWeatherData = databaseWeatherDataList.OrderByDescending(data => data.DateTime).FirstOrDefault();

        //        // Check if the database contains any entries
        //        if (latestDatabaseWeatherData == null)
        //        {
        //            // If no data in the database, add the latest from API
        //            await AddWeatherDataToDatabase(latestApiWeatherData);
        //            Console.WriteLine("Database was empty. Added new weather data.");
        //            return;
        //        }

        //        // Compare the DateTime values
        //        if (latestApiWeatherData.ForecastTime != latestDatabaseWeatherData.ForecastTime)
        //        {
        //            // If the latest data from the API is more recent, add it to the database
        //            await AddWeatherDataToDatabase(latestApiWeatherData);
        //            Console.WriteLine("New weather data added to the database.");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Database is up to date. No new data added.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error updating weather data: {ex.Message}");
        //    }
        //}
    }
}

