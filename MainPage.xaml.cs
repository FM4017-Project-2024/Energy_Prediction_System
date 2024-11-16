using Microsoft.Maui.Controls;
using Energy_Prediction_System.Views;
using Energy_Prediction_System.Services;
using Energy_Prediction_System.Classes;
using System.Runtime.CompilerServices;
using System;
using Microsoft.Maui.Controls.Shapes;


namespace Energy_Prediction_System
{
    public partial class MainPage : ContentPage
    {
        private float _gaugeValue;
        private bool _isRunning = false;
        public MainPage()
        {
            
            InitializeComponent();
            WriteData();
        }

        private async void OnLiveSensorData1ButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new LiveSensorData());
        private async void OnLiveWeatherData1ButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new LiveWheaterData());
        private async void OnDatabaseViewPageButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new DatabaseViewPage());
        private async void OnOpenDatabasePostPageClicked(object sender, EventArgs e) => await Navigation.PushAsync(new DatabasePostPage());
        private async void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = pageSelector.SelectedIndex;
            switch (selectedIndex)
            {
                case 0:
                    await Navigation.PushAsync(new LiveSensorData());
                    break;
                case 1:
                    await Navigation.PushAsync(new LiveWheaterData());
                    break;
                case 2:
                    await Navigation.PushAsync(new Energy());
                    break;
                default:
                    break;
            }
            pageSelector.SelectedIndex = -1;
        }

        private void UpdateGUI(double TT_AVG, double RHT_AVG, string TT_OUT, string RH_OUT, double KWH_CURR, double KWH_PRED)
        {

            MainThread.BeginInvokeOnMainThread(() =>
            {
                TempAvg_Label.Text = $"Temperature: {TT_AVG:F2}" + " °C";
                HumpAvg_Label.Text = $"Humidity: {RHT_AVG:F2}" + " %";
                OutsideTemp_Label.Text = $"Temperature: {TT_OUT:F2}" + " °C";
                OutsideHum_Label.Text = $"Humidity: {RH_OUT:F2}" + " %";
                PredEnergy_Label.Text = $"Predicted: {KWH_PRED:F2}" + " kWh";
                CurrEnergy_Label.Text = $"Current: {KWH_CURR:F2}" + " kWh";

            });
        }

        private async void WriteData()
        {
            while (_isRunning)
            {
                var sensorData = App.SensorValues;
                var weatherData = App.WeatherValues;

                double TT_AVG = sensorData.sensor.TT_AVG;
                double RHT_AVG = sensorData.sensor.RHT_AVG;
                double KWH_CURR = sensorData.sensor.KWH01;
                double KWH_PRED = sensorData.sensor.Current_Prediciton;
                string TT_out = weatherData.weather.TTT;
                string RH_out = weatherData.weather.NA;


                UpdateGUI(TT_AVG, RHT_AVG, TT_out, RH_out, KWH_CURR, KWH_PRED);

                await Task.Delay(3000);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _isRunning = true;
            WriteData();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _isRunning = false;
        }

        private async void OnLabelTapped_AvgTemp(object sender, EventArgs e) => await Navigation.PushAsync(new LiveSensorData());
        private async void OnLabelTapped_AvgHum(object sender, EventArgs e) => await Navigation.PushAsync(new LiveSensorData()); 
        private async void OnLabelTapped_OutTemp(object sender, EventArgs e) => await Navigation.PushAsync(new LiveWheaterData());
        private async void OnLabelTapped_OutHum(object sender, EventArgs e) => await Navigation.PushAsync(new LiveWheaterData());
        private async void OnLabelTapped_Energy_Predicted(object sender, EventArgs e) => await Navigation.PushAsync(new Energy());
        private async void OnLabelTapped_Energy_Current(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("Energy meter", "Energy consumption[kWh]", App.SensorValues.historical.KWH01, App.SensorValues.sensor.KWH_DT));
    }
}

