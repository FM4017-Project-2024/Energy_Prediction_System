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
            writeData();
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
                    await Navigation.PushAsync(new DatabaseViewPage());
                    break;
                case 3:
                    await Navigation.PushAsync(new DatabasePostPage());
                    break;
                default:
                    break;
            }
            pageSelector.SelectedIndex = -1; // Resets the picker to no selection
        }

        private void UpdateGUI(float rand)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                TempAvg_Label.Text = $"Temperature: {rand:F2}" + " °C";
                HumpAvg_Label.Text = $"Humidity: {rand:F2}" + " %";
                OutsideTemp_Label.Text = $" {rand:F2}" + " °C";
                PredEnergy_Label.Text = $" {rand:F2}" + " kWh";
            });
        }

        private async void writeData()
        {
            while (_isRunning)
            {
                var random = new Random();
                UpdateGUI(random.Next(0, 100));
                await Task.Delay(3000);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _isRunning = true;
            writeData();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _isRunning = false;
        }
    }
}

