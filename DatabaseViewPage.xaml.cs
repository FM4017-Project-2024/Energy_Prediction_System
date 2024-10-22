using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Energy_Prediction_System.Services;
using Energy_Prediction_System.Classes;

namespace Energy_Prediction_System.Views
{
    public partial class DatabaseViewPage : ContentPage
    {
        private readonly DatabaseWebAPIServices _databaseWebAPIServices;

        public DatabaseViewPage()
        {
            InitializeComponent();
            _databaseWebAPIServices = new DatabaseWebAPIServices();
        }

        // Knappehendelser for å hente alle temperaturdata
        private async void OnGetAllBuildingTempsClicked(object sender, EventArgs e)
        {
            string apiUrl = "https://localhost:7107/api/BuildingTemperatureItems"; // Eksempel URL for API
            try
            {
                var temperatureItems = await _databaseWebAPIServices.GetBuildingTempsAsync(apiUrl);
                TemperatureListView.ItemsSource = temperatureItems;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to fetch temperature data: {ex.Message}", "OK");
            }
        }

        // Knappehendelser for å hente den siste temperaturmålingen
        private async void OnGetLatestBuildingTempClicked(object sender, EventArgs e)
        {
            string apiUrl = "https://localhost:7107/api/BuildingTemperatureItems/latest"; // Eksempel URL for API
            try
            {
                var latestTemp = await _databaseWebAPIServices.GetLatestBuildingTempAsync(apiUrl);
                TemperatureListView.ItemsSource = new List<BuildingTemperatureItem> { latestTemp };
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to fetch latest temperature: {ex.Message}", "OK");
            }
        }

        // Knappehendelser for å hente all fuktighetsdata
        private async void OnGetAllBuildingRelHumidityClicked(object sender, EventArgs e)
        {
            string apiUrl = "https://localhost:7107/api/BuildingRelativeHumidityItems"; // Eksempel URL for API
            try
            {
                var humidityItems = await _databaseWebAPIServices.GetBuildingRelHumidityAsync(apiUrl);
                HumidityListView.ItemsSource = humidityItems;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to fetch humidity data: {ex.Message}", "OK");
            }
        }

        // Knappehendelser for å hente den siste fuktighetsmålingen
        private async void OnGetLatestBuildingRelHumidityClicked(object sender, EventArgs e)
        {
            string apiUrl = "https://localhost:7107/api/BuildingRelativeHumidityItems/latest"; // Eksempel URL for API
            try
            {
                var latestHumidity = await _databaseWebAPIServices.GetLatestBuildingRelHumidityAsync(apiUrl);
                HumidityListView.ItemsSource = new List<BuildingRelativeHumidityItem> { latestHumidity };
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to fetch latest humidity: {ex.Message}", "OK");
            }
        }

        // Håndter knappetrykk for å hente alle energimålerdata
        private async void OnGetAllEnergyMeterDataClicked(object sender, EventArgs e)
        {
            string apiUrl = "https://localhost:7107/api/BuildingEnergyMeterItems"; // Endre til ditt API-endepunkt
            try
            {
                // Hent alle data
                var energyMeterItems = await _databaseWebAPIServices.GetBuildingEnergyMeterAsync(apiUrl);
                // Sett dataene til å vises i ListView
                EnergyMeterListView.ItemsSource = energyMeterItems;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to fetch energy meter data: {ex.Message}", "OK");
            }
        }

        // Håndter knappetrykk for å hente den siste energimålingen
        private async void OnGetLatestEnergyMeterDataClicked(object sender, EventArgs e)
        {
            string apiUrl = "https://localhost:7107/api/BuildingEnergyMeterItems/latest"; // Endre til ditt API-endepunkt
            try
            {
                // Hent siste energimåling
                var latestEnergyMeter = await _databaseWebAPIServices.GetLatestBuildingEnergyMeterAsync(apiUrl);
                // Vis den siste målingen som en liste med ett element
                EnergyMeterListView.ItemsSource = new List<BuildingEnergyMeterItem> { latestEnergyMeter };
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to fetch latest energy meter data: {ex.Message}", "OK");
            }
        }
    }
}