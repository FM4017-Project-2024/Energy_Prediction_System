using Energy_Prediction_System.Classes;

namespace Energy_Prediction_System;

public partial class LiveSensorData : ContentPage
{
    private bool _isRunning = true;
    private readonly SensorSim rndVal = new();
    private bool _simulate = true;

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
        StartSimulatingSensorData();
    }
    private async void StartSimulatingSensorData()
    {
        while (_isRunning)
        {
            await Task.Delay(2000);


            if (_simulate)
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
            }
            else
            {
                // Read from webAPI or something
            }
            MainThread.BeginInvokeOnMainThread(() =>
            {
                TT01_Label.Text = $"TT01: {sensor.TT01:F2}";
                TT02_Label.Text = $"TT02: {sensor.TT02:F2}";
                TT03_Label.Text = $"TT03: {sensor.TT03:F2}";
                TT04_Label.Text = $"TT04: {sensor.TT04:F2}";
                TT05_Label.Text = $"TT05: {sensor.TT05:F2}";
                RHT01_Label.Text = $"RHT01: {sensor.RHT01:F2}";
                RHT02_Label.Text = $"RHT02: {sensor.RHT02:F2}";
                RHT03_Label.Text = $"RHT03: {sensor.RHT03:F2}";
                RHT04_Label.Text = $"RHT04: {sensor.RHT04:F2}";
                KWH_Label.Text = $"KWH01: {sensor.KWH01:F2}";
            });
        }
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _isRunning = false;
    }
}