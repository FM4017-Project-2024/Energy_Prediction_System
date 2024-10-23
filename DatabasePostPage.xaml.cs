using Microsoft.Maui.Controls;
using System;
using Energy_Prediction_System.Services;
using Energy_Prediction_System.Classes;

namespace Energy_Prediction_System.Views
{
    public partial class DatabasePostPage : ContentPage
    {
        private readonly DatabaseWebAPIServices _databaseWebAPIServices;

        public DatabasePostPage()
        {
            InitializeComponent();
            _databaseWebAPIServices = new DatabaseWebAPIServices();
        }

        // Håndter POST for temperaturdata
        private async void OnPostTemperatureClicked(object sender, EventArgs e)
        {
            string apiUrl = "https://localhost:7107/api/BuildingTemperatureItems"; // Oppdatert URL

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

        // Håndter POST for fuktighetsdata
        private async void OnPostHumidityClicked(object sender, EventArgs e)
        {
            string apiUrl = "https://localhost:7107/api/BuildingRelativeHumidityItems"; // Oppdatert URL

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

        // Håndter POST for energimålerdata
        private async void OnPostEnergyMeterClicked(object sender, EventArgs e)
        {
            string apiUrl = "https://localhost:7107/api/BuildingEnergyMeterItems"; // Oppdatert URL

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
    }
}