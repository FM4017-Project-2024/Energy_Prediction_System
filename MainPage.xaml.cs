﻿namespace Energy_Prediction_System
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private async void OnLiveSensorData1ButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new LiveSensorData());
        private async void OnLiveWeatherData1ButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new LiveWheaterData());
    }
}

