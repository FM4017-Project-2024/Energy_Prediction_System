using Energy_Prediction_System.Services;
using Energy_Prediction_System.Classes;
using System.Diagnostics;

namespace Energy_Prediction_System;

public partial class LiveSensorData : ContentPage
{
    private readonly SensorSim rndVal = new();

    private bool _simulate = false;
    private bool _simulationStarted = false;
    private bool _isRunning = false;

    public LiveSensorData()
    {
        InitializeComponent();
        getSensorData();
    }
    private async void getSensorData()
    {
        while (_isRunning)
        {
            if (_simulate)
            {
                UpdateGUI();
                await Task.Delay(3000);
            }
            else
            {
                UpdateGUI();
                await Task.Delay(20000);
            }
            
        }
    }

    private void UpdateGUI()
    {
        var sensorData = App.SensorValues;
        MainThread.BeginInvokeOnMainThread(() =>
        {
            if(_simulate)
            {
                TT01_Label.Text = rndVal.GetRandomDouble(0, 20).ToString();
                TT02_Label.Text = rndVal.GetRandomDouble(0, 10).ToString();
                TT03_Label.Text = rndVal.GetRandomDouble(0, 30).ToString();
                TT04_Label.Text = rndVal.GetRandomDouble(0, 30).ToString();
                TT05_Label.Text = rndVal.GetRandomDouble(0, 30).ToString();
                RHT01_Label.Text = rndVal.GetRandomDouble(0, 30).ToString();
                RHT02_Label.Text = rndVal.GetRandomDouble(0, 30).ToString();
                RHT03_Label.Text = rndVal.GetRandomDouble(0, 30).ToString();
                RHT04_Label.Text = rndVal.GetRandomDouble(0, 30).ToString();
                KWH_Label.Text = rndVal.GetRandomDouble(0, 30).ToString();
            }
            else
            {
                TT01_Label.Text = $"First floor: {sensorData.sensor.TT01:F2}" + " °C";
                TT02_Label.Text = $"Second floor: {sensorData.sensor.TT02:F2}" + " °C";
                TT03_Label.Text = $"Office: {sensorData.sensor.TT03:F2}" + " °C";
                TT04_Label.Text = $"Guest room: {sensorData.sensor.TT04:F2}" + " °C";
                TT05_Label.Text = $"Kitchen: {sensorData.sensor.TT05:F2}" + " °C";
                TTDT_Label.Text = sensorData.sensor.TT_DT.ToString();
                RHT01_Label.Text = $"First floor: {sensorData.sensor.RHT01:F2}" + " %";
                RHT02_Label.Text = $"Second floor: {sensorData.sensor.RHT02:F2}" + " %";
                RHT03_Label.Text = $"Guest room: {sensorData.sensor.RHT03:F2}" + " %";
                RHT04_Label.Text = $"Office: {sensorData.sensor.RHT04:F2}" + " %";
                RHTDT_Label.Text = sensorData.sensor.RHT_DT.ToString();
                KWH_Label.Text = $"Energy meter: {sensorData.sensor.KWH01:F2}" + " kWh";
                KWHDT_Label.Text = sensorData.sensor.KWH_DT.ToString();
            }
        });
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _isRunning = false;
        _simulationStarted = false;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _isRunning = true;
        if (!_simulationStarted)
        {
            _simulationStarted = true;
            getSensorData();
        }
    }

    private async void OnLabelTapped_TT01(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("First floor", "Temperature[°C]", App.SensorValues.historical.TT01, App.SensorValues.sensor.TT_DT));
    private async void OnLabelTapped_TT02(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("Second floor", "Temperature[°C]", App.SensorValues.historical.TT02, App.SensorValues.sensor.TT_DT));
    private async void OnLabelTapped_TT03(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("Office", "Temperature[°C]", App.SensorValues.historical.TT03, App.SensorValues.sensor.TT_DT));
    private async void OnLabelTapped_TT04(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("Guestroom", "Temperature[°C]", App.SensorValues.historical.TT04, App.SensorValues.sensor.TT_DT));
    private async void OnLabelTapped_TT05(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("Kitchen", "Temperature[°C]", App.SensorValues.historical.TT05, App.SensorValues.sensor.TT_DT));
    private async void OnLabelTapped_RHT01(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("First floor", "Relative humidity[%]", App.SensorValues.historical.RHT01, App.SensorValues.sensor.RHT_DT));
    private async void OnLabelTapped_RHT02(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("Second floor", "Relative humidity[%]", App.SensorValues.historical.RHT02, App.SensorValues.sensor.RHT_DT));
    private async void OnLabelTapped_RHT03(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("Guestroom", "Relative humidity[%]", App.SensorValues.historical.RHT03, App.SensorValues.sensor.RHT_DT));
    private async void OnLabelTapped_RHT04(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("Office", "Relative humidity[%]", App.SensorValues.historical.RHT04, App.SensorValues.sensor.RHT_DT));
    private async void OnLabelTapped_KWH01(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("Energy meter","Energy consumption[kWh]", App.SensorValues.historical.KWH01, App.SensorValues.sensor.KWH_DT));
}