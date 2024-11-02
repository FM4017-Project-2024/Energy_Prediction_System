using Microsoft.Maui.Controls;
using System;
using Energy_Prediction_System.Services;
using Energy_Prediction_System.Classes;

namespace Energy_Prediction_System.Views
{
    public partial class DatabasePostPage : ContentPage
    {
        private readonly DatabaseWebAPIServices _databaseWebAPIServices;
        private const string BaseApiUrl = "https://localhost:7107/api/";

        public DatabasePostPage()
        {
            InitializeComponent();
            _databaseWebAPIServices = new DatabaseWebAPIServices();
        }

        // Handle POST for temperature data
        private async void OnPostTemperatureClicked(object sender, EventArgs e)
        {
            string apiUrl = $"{BaseApiUrl}BuildingTemperatureItems";

            var temperatureItem = new BuildingTemperatureItem
            {
                Temp1 = float.Parse(TempEntry1.Text),
                Temp2 = float.Parse(TempEntry2.Text),
                Temp3 = float.Parse(TempEntry3.Text),
                Temp4 = float.Parse(TempEntry4.Text),
                Temp5 = float.Parse(TempEntry5.Text),
                TempUoM = TempUoMEntry.Text,
                TempDateTime = DateTime.Now
            };

            try
            {
                var response = await _databaseWebAPIServices.PostBuildingTemperatureAsync(apiUrl, temperatureItem);
                ApiResponseLabel.Text = "Success: " + response;
            }
            catch (Exception ex)
            {
                ApiResponseLabel.Text = "Error: " + ex.Message;
            }
        }

        // Handle POST for humidity data
        private async void OnPostHumidityClicked(object sender, EventArgs e)
        {
            string apiUrl = $"{BaseApiUrl}BuildingRelativeHumidityItems";

            var humidityItem = new BuildingRelativeHumidityItem
            {
                RelHumidity1 = float.Parse(HumidityEntry1.Text),
                RelHumidity2 = float.Parse(HumidityEntry2.Text),
                RelHumidity3 = float.Parse(HumidityEntry3.Text),
                RelHumidity4 = float.Parse(HumidityEntry4.Text),
                RelHumidityUoM = HumidityUoMEntry.Text,
                RelHumidityDateTime = DateTime.Now
            };

            try
            {
                var response = await _databaseWebAPIServices.PostBuildingRelativeHumidityAsync(apiUrl, humidityItem);
                ApiResponseLabel.Text = "Success: " + response;
            }
            catch (Exception ex)
            {
                ApiResponseLabel.Text = "Error: " + ex.Message;
            }
        }

        // Handle POST for energy meter data
        private async void OnPostEnergyMeterClicked(object sender, EventArgs e)
        {
            string apiUrl = $"{BaseApiUrl}BuildingEnergyMeterItems";

            var energyMeterItem = new BuildingEnergyMeterItem
            {
                EnergyMeter1 = float.Parse(EnergyMeterEntry1.Text),
                EnergyMeterUoM = EnergyMeterUoMEntry.Text,
                EnergyMeterDateTime = DateTime.Now
            };

            try
            {
                var response = await _databaseWebAPIServices.PostBuildingEnergyMeterAsync(apiUrl, energyMeterItem);
                ApiResponseLabel.Text = "Success: " + response;
            }
            catch (Exception ex)
            {
                ApiResponseLabel.Text = "Error: " + ex.Message;
            }
        }

        // Handle POST for energy predictions
        private async void OnPostEnergyPredictionClicked(object sender, EventArgs e)
        {
            string apiUrl = $"{BaseApiUrl}EnergyPredictionItems";

            var energyPredictionItem = new EnergyPredictionItem
            {
                EnergyPrediction = float.Parse(EnergyPredictionEntry.Text),
                EnergyPredictionUoM = EnergyPredictionUoMEntry.Text,
                DateTime = DateTime.Now
            };

            try
            {
                var response = await _databaseWebAPIServices.PostEnergyPredictionAsync(apiUrl, energyPredictionItem);
                ApiResponseLabel.Text = "Success: " + response;
            }
            catch (Exception ex)
            {
                ApiResponseLabel.Text = "Error: " + ex.Message;
            }
        }

        // Handle POST for weather forecast unit of measure (UoM)
        private async void OnPostWeatherForecastUoMClicked(object sender, EventArgs e)
        {
            string apiUrl = $"{BaseApiUrl}WeatherForecastUoMItems";

            var weatherForecastUoMItem = new WeatherForecastUoMItem
            {
                Attribute = AttributeEntry.Text,
                UoM = UoMEntry.Text
            };

            try
            {
                var response = await _databaseWebAPIServices.PostWeatherForecastUoMAsync(apiUrl, weatherForecastUoMItem);
                ApiResponseLabel.Text = "Success: " + response;
            }
            catch (Exception ex)
            {
                ApiResponseLabel.Text = "Error: " + ex.Message;
            }
        }
    }
}