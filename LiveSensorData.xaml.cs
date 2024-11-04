using Energy_Prediction_System.Classes;

namespace Energy_Prediction_System;

public partial class LiveSensorData : ContentPage
{
    private readonly SensorSim rndVal = new();

    private bool _simulate = true;
    private bool _simulationStarted = false;
    private bool _isRunning = false;

    private struct Livesensors 
    {
        public double TT01 { get; set; }
        public double TT02 { get; set; }
        public double TT03 { get; set; }
        public double TT04 { get; set; }
        public double TT05 { get; set; }
        public double RHT01 { get; set; }
        public double RHT02 { get; set; }
        public double RHT03 { get; set; }
        public double RHT04 { get; set; }
        public double KWH01 { get; set; }
    }
    Livesensors sensor = new Livesensors();

    public LiveSensorData()
    {
        InitializeComponent();
        getSensorData();
    }
    private async void getSensorData()
    {
        while (_isRunning)
        {
            await Task.Delay(3000);
            if (_simulate)
            {
                StartSimulatingSensorData();
            }
        }
    }

    private void UpdateGUI()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            TT01_Label.Text = $"TT01: {sensor.TT01:F2}" + " °C";
            TT02_Label.Text = $"TT02: {sensor.TT02:F2}" + " °C";
            TT03_Label.Text = $"TT03: {sensor.TT03:F2}" + " °C";
            TT04_Label.Text = $"TT04: {sensor.TT04:F2}" + " °C";
            TT05_Label.Text = $"TT05: {sensor.TT05:F2}" + " °C";
            RHT01_Label.Text = $"RHT01: {sensor.RHT01:F2}" + " %";
            RHT02_Label.Text = $"RHT02: {sensor.RHT02:F2}" + " %";
            RHT03_Label.Text = $"RHT03: {sensor.RHT03:F2}" + " %";
            RHT04_Label.Text = $"RHT04: {sensor.RHT04:F2}" + " %";
            KWH_Label.Text = $"KWH01: {sensor.KWH01:F2}" + " kWh";
        });
    }

    private void StartSimulatingSensorData()
    {
        sensor.TT01 = rndVal.GetRandomDouble(0, 20);
        sensor.TT02 = rndVal.GetRandomDouble(0, 10);
        sensor.TT03 = rndVal.GetRandomDouble(0, 30);
        sensor.TT04 = rndVal.GetRandomDouble(0, 30);
        sensor.TT05 = rndVal.GetRandomDouble(0, 30);
        sensor.RHT01 = rndVal.GetRandomDouble(0, 30);
        sensor.RHT02 = rndVal.GetRandomDouble(0, 30);
        sensor.RHT03 = rndVal.GetRandomDouble(0, 30);
        sensor.RHT04 = rndVal.GetRandomDouble(0, 30);
        sensor.KWH01 = rndVal.GetRandomDouble(0, 30);

        UpdateGUI();
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

    private async void OnLabelTapped_TT01(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("TT01"));
    private async void OnLabelTapped_TT02(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("TT02"));
    private async void OnLabelTapped_TT03(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("TT03"));
    private async void OnLabelTapped_TT04(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("TT04"));
    private async void OnLabelTapped_TT05(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("TT05"));
    private async void OnLabelTapped_RHT01(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("RHT01"));
    private async void OnLabelTapped_RHT02(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("RHT02"));
    private async void OnLabelTapped_RHT03(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("RHT03"));
    private async void OnLabelTapped_RHT04(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("RHT04"));
    private async void OnLabelTapped_KWH01(object sender, EventArgs e) => await Navigation.PushAsync(new HistoricalData_1("KWH01"));
}