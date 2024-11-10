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

        // Methods to retrieve temperature data
        private async void OnGetAllBuildingTempsClicked(object sender, EventArgs e)
        {
            string apiUrl = "/api/BuildingTemperatureItems";
            try
            {
                var temperatureItems = await _databaseWebAPIServices.GetBuildingTempsAsync(apiUrl);
                TemperatureListView.ItemsSource = temperatureItems;
            }
            catch (Exception ex)
            {
                await HandleError(ex, "Failed to fetch temperature data");
            }
        }

        private async void OnGetLatestBuildingTempClicked(object sender, EventArgs e)
        {
            string apiUrl = "/api/BuildingTemperatureItems/latest";
            try
            {
                var latestTemp = await _databaseWebAPIServices.GetLatestBuildingTempAsync(apiUrl);
                TemperatureListView.ItemsSource = new List<BuildingTemperatureItem> { latestTemp };
            }
            catch (Exception ex)
            {
                await HandleError(ex, "Failed to fetch latest temperature");
            }
        }

        // Methods to retrieve humidity data
        private async void OnGetAllBuildingRelHumidityClicked(object sender, EventArgs e)
        {
            string apiUrl = "/api/BuildingRelativeHumidityItems";
            try
            {
                var humidityItems = await _databaseWebAPIServices.GetBuildingRelHumidityAsync(apiUrl);
                HumidityListView.ItemsSource = humidityItems;
            }
            catch (Exception ex)
            {
                await HandleError(ex, "Failed to fetch humidity data");
            }
        }

        private async void OnGetLatestBuildingRelHumidityClicked(object sender, EventArgs e)
        {
            string apiUrl = "/api/BuildingRelativeHumidityItems/latest";
            try
            {
                var latestHumidity = await _databaseWebAPIServices.GetLatestBuildingRelHumidityAsync(apiUrl);
                HumidityListView.ItemsSource = new List<BuildingRelativeHumidityItem> { latestHumidity };
            }
            catch (Exception ex)
            {
                await HandleError(ex, "Failed to fetch latest humidity");
            }
        }

        // Methods to retrieve energy meter data
        private async void OnGetAllEnergyMeterDataClicked(object sender, EventArgs e)
        {
            string apiUrl = "/api/BuildingEnergyMeterItems";
            try
            {
                var energyMeterItems = await _databaseWebAPIServices.GetBuildingEnergyMeterAsync(apiUrl);
                EnergyMeterListView.ItemsSource = energyMeterItems;
            }
            catch (Exception ex)
            {
                await HandleError(ex, "Failed to fetch energy meter data");
            }
        }

        private async void OnGetLatestEnergyMeterDataClicked(object sender, EventArgs e)
        {
            string apiUrl = "/api/BuildingEnergyMeterItems/latest";
            try
            {
                var latestEnergyMeter = await _databaseWebAPIServices.GetLatestBuildingEnergyMeterAsync(apiUrl);
                EnergyMeterListView.ItemsSource = new List<BuildingEnergyMeterItem> { latestEnergyMeter };
            }
            catch (Exception ex)
            {
                await HandleError(ex, "Failed to fetch latest energy meter data");
            }
        }

        // Methods to retrieve energy prediction data
        private async void OnGetAllEnergyPredictionsClicked(object sender, EventArgs e)
        {
            string apiUrl = "/api/EnergyPredictionItems";
            try
            {
                var energyPredictionItems = await _databaseWebAPIServices.GetEnergyPredictionsAsync(apiUrl);
                EnergyPredictionListView.ItemsSource = energyPredictionItems;
            }
            catch (Exception ex)
            {
                await HandleError(ex, "Failed to fetch energy predictions");
            }
        }

        private async void OnGetLatestEnergyPredictionClicked(object sender, EventArgs e)
        {
            string apiUrl = "/api/EnergyPredictionItems/latest";
            try
            {
                var latestPrediction = await _databaseWebAPIServices.GetLatestEnergyPredictionAsync(apiUrl);
                EnergyPredictionListView.ItemsSource = latestPrediction;
            }
            catch (Exception ex)
            {
                await HandleError(ex, "Failed to fetch latest energy prediction");
            }
        }

        // Methods to retrieve weather forecast data
        private async void OnGetAllWeatherForecastsClicked(object sender, EventArgs e)
        {
            string apiUrl = "/api/WeatherForecastItems";
            try
            {
                var weatherForecastItems = await _databaseWebAPIServices.GetWeatherForecastsAsync(apiUrl);
                WeatherForecastListView.ItemsSource = weatherForecastItems;
            }
            catch (Exception ex)
            {
                await HandleError(ex, "Failed to fetch weather forecasts");
            }
        }

        private async void OnGetLatestWeatherForecastClicked(object sender, EventArgs e)
        {
            string apiUrl = "/api/WeatherForecastItems/latest";
            try
            {
                var latestForecast = await _databaseWebAPIServices.GetLatestWeatherForecastsAsync(apiUrl);
                WeatherForecastListView.ItemsSource = latestForecast;
            }
            catch (Exception ex)
            {
                await HandleError(ex, "Failed to fetch latest weather forecast");
            }
        }

        // Methods to retrieve weather forecast units of measurement (UoM)
        private async void OnGetAllWeatherForecastUoMsClicked(object sender, EventArgs e)
        {
            string apiUrl = "/api/WeatherForecastUoMItems";
            try
            {
                var uomItems = await _databaseWebAPIServices.GetWeatherForecastUoMsAsync(apiUrl);
                WeatherForecastUoMListView.ItemsSource = uomItems;
            }
            catch (Exception ex)
            {
                await HandleError(ex, "Failed to fetch weather forecast UoM data");
            }
        }

        // Retrieve UoM for a specific attribute
        private async void OnGetUoMForAttributeClicked(object sender, EventArgs e)
        {
            string apiUrl = "/api/WeatherForecastUoMItems/uom/";

            try
            {
                var attribute = AttributeEntry.Text;
                var uom = await _databaseWebAPIServices.GetUoMForAttributeAsync(apiUrl, attribute);

                if (!string.IsNullOrEmpty(uom))
                {
                    // Create a WeatherForecastUoMItem with the attribute and UoM
                    var uomItem = new WeatherForecastUoMItem
                    {
                        Attribute = attribute,
                        UoM = uom
                    };

                    // Set the WeatherForecastUoMListView to display the single item
                    WeatherForecastUoMListView.ItemsSource = new List<WeatherForecastUoMItem> { uomItem };
                }
                else
                {
                    // Clear the ListView if no UoM is found
                    WeatherForecastUoMListView.ItemsSource = null;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Failed to retrieve UoM: " + ex.Message, "OK");
            }
        }

        // Error handling method
        private async Task HandleError(Exception ex, string message)
        {
            await DisplayAlert("Error", $"{message}: {ex.Message}", "OK");
        }
    }
}