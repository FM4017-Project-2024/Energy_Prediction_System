using Energy_Prediction_System.Classes;

namespace Energy_Prediction_System;

public partial class LiveSensorData : ContentPage
{
    private bool _isRunning = true;
    private readonly SensorSim rndVal = new();
    private bool _simulate = true;

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

            double TT01 = 0;
            double TT02 = 0;
            double TT03 = 0;
            double TT04 = 0;
            double TT05 = 0;
            double RHT01 = 0;
            double RHT02 = 0;
            double RHT03 = 0;
            double RHT04 = 0;
            double KWH01 = 0;

            if (_simulate)
            {
                TT01 = rndVal.GetRandomDouble(0, 20);
                TT02 = rndVal.GetRandomDouble(0, 10);
                TT03 = rndVal.GetRandomDouble(0, 30);
                TT04 = rndVal.GetRandomDouble(0, 30);
                TT05 = rndVal.GetRandomDouble(0, 30);
                RHT01 = rndVal.GetRandomDouble(0, 30);
                RHT02 = rndVal.GetRandomDouble(0, 30);
                RHT03 = rndVal.GetRandomDouble(0, 30);
                RHT04 = rndVal.GetRandomDouble(0, 30);
                KWH01 = rndVal.GetRandomDouble(0, 30);
            }
            else
            {
                // Read from webAPI or something
            }
            MainThread.BeginInvokeOnMainThread(() =>
            {
                TT01_Label.Text = $"TT01: {TT01:F2}";
                TT02_Label.Text = $"TT02: {TT02:F2}";
                TT03_Label.Text = $"TT03: {TT03:F2}";
                TT04_Label.Text = $"TT04: {TT04:F2}";
                TT05_Label.Text = $"TT05: {TT05:F2}";
                RHT01_Label.Text = $"RHT01: {RHT01:F2}";
                RHT02_Label.Text = $"RHT02: {RHT02:F2}";
                RHT03_Label.Text = $"RHT03: {RHT03:F2}";
                RHT04_Label.Text = $"RHT04: {RHT04:F2}";
                KWH_Label.Text = $"KWH01: {KWH01:F2}";
            });
        }
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _isRunning = false;
    }
}