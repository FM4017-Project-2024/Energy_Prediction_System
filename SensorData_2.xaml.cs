using Energy_Prediction_System.Classes;

namespace Energy_Prediction_System;

public partial class SensorData_2 : ContentPage
{
    private bool _isRunning = true;
    private readonly SensorSim rndVal = new();

    public SensorData_2()
    {
        InitializeComponent();
        StartSimulatingSensorData();
    }
    private async void StartSimulatingSensorData()
    {
        while (_isRunning)
        {
            await Task.Delay(1000);

            double temp1 = rndVal.GetRandomDouble(0, 20);
            double temp2 = rndVal.GetRandomDouble(0, 10);
            double temp3 = rndVal.GetRandomDouble(0, 30);
            double temp4 = rndVal.GetRandomDouble(-20, 20);
            double temp5 = rndVal.GetRandomDouble(0, 5);
            double temp6 = rndVal.GetRandomDouble(0, 33);
            double temp7 = rndVal.GetRandomDouble(0, 45);
            MainThread.BeginInvokeOnMainThread(() =>
            {
                T1Label.Text = $"T_1: {temp1:F2}";
                T2Label.Text = $"T_2: {temp2:F2}";
                T3Label.Text = $"T_3: {temp3:F2}";
                T4Label.Text = $"T_4: {temp4:F2}";
                T5Label.Text = $"T_5: {temp5:F2}";
                T6Label.Text = $"T_6: {temp6:F2}";
                T7Label.Text = $"T_7: {temp7:F2}";
            });
        }
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _isRunning = false;
    }
}